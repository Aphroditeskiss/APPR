using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Media;

namespace APPR_ChessPiecesAsChessPieces_22SD_DaphnevanRooij
{ // ====================== VARIABLES ======================
    public partial class Gameform : Form //main game window
    {
        private Piece currentPiece = null;
        private PictureBox pcbFrom = null;
        private PictureBox pcbTo = null;
        private int horizontal = 0;
        private int vertical = 0;
        private string pieceOptions = "";

        private List<Board> boardList = new List<Board>();
        private List<string> whitePlacedPieces = new List<string>(); //tracks white pieces
        private List<string> blackPlacedPieces = new List<string>(); //tracks black pieces
        private Dictionary<string, Piece> boardPieces = new Dictionary<string, Piece>();

        private string currentPlayer = "White";
        private bool isPlacementPhase = true;
        private int piecesPlaced = 0;
        private PictureBox currentlySelectedPiece = null;

        // ====================== ADVANCED ARDUINO ROBOT CONTROL ======================
        private int moveArduinoCounter = 0;
        private bool moveBusy = false;
        private Location currentFromLocation = null;
        private Location currentToLocation = null;
        // ====================== ARDUINO INTEGRATION ======================
        private Arduinoform arduinoForm = null;
        private List<Location> locationList = new List<Location>();

        private SoundPlayer soundPlayer = new SoundPlayer();

        public Gameform()
        {
            InitializeComponent();
        }

        //====================== GAME INITIALIZATION ======================

        private void Gameform_Load(object sender, EventArgs e)
        {
            // Initialize Arduino Form
            arduinoForm = new Arduinoform(this);
            arduinoForm.Owner = this;   // So it stays on top of game

            // ====================== ROBOT ARM LOCATIONS (REAL SERVO VALUES) ======================
            // ====================== ROBOT ARM LOCATIONS - CORRECT COORDINATES ======================
            locationList.Clear();

            // Row 1 (White's side) - using 1-based indexing
            locationList.Add(new Location(1, 1, 250, 520, 1300));   // row1 col1
            locationList.Add(new Location(1, 2, 210, 1000, 1300));  // row1 col2  
            locationList.Add(new Location(1, 3, 185, 1500, 1300));  // row1 col3

            // Row 2 (Middle)
            locationList.Add(new Location(2, 1, 140, 370, 1300));
            locationList.Add(new Location(2, 2, 130, 850, 1300));   // center
            locationList.Add(new Location(2, 3, 95, 1350, 1300));

            // Row 3 (Black's side)
            locationList.Add(new Location(3, 1, 25, 300, 1300));
            locationList.Add(new Location(3, 2, 25, 850, 1300));
            locationList.Add(new Location(3, 3, 25, 1350, 1300));   // row3 col3

            // setup board squares
            foreach (PictureBox pb in gbxPlayingField.Controls.OfType<PictureBox>())
            {
                pb.BackColor = Color.LightGray;
                pb.SizeMode = PictureBoxSizeMode.StretchImage;
                pb.AllowDrop = true;
            }

            // initialize board positions
            boardList.Clear();
            boardList.Add(new Board(1, 1, "pcbOne"));
            boardList.Add(new Board(2, 1, "pcbTwo"));
            boardList.Add(new Board(3, 1, "pcbThree"));
            boardList.Add(new Board(1, 2, "pcbFour"));
            boardList.Add(new Board(2, 2, "pcbFive"));
            boardList.Add(new Board(3, 2, "pcbSix"));
            boardList.Add(new Board(1, 3, "pcbSeven"));
            boardList.Add(new Board(2, 3, "pcbEight"));
            boardList.Add(new Board(3, 3, "pcbNine"));

            //connect radio buttens
            radioWhite.CheckedChanged += radioPlayer_CheckedChanged;
            radioBlack.CheckedChanged += radioPlayer_CheckedChanged;

            isPlacementPhase = true;
            currentPlayer = "White";
            piecesPlaced = 0;
            radioWhite.Checked = true;

            HighlightStartingRow();
            UpdateSidebarVisibility();
        }

        // ====================== SIDEBAR VISIBILITY ======================

        private void UpdateSidebarVisibility() //hide sidebar pieces and only show current players pieces
        {
            if (pcbWhiteRook == null || pcbBlackRook == null) return;

            pcbWhiteRook.Visible = false;
            pcbWhiteKnight.Visible = false;
            pcbWhiteQueen.Visible = false;
            pcbWhiteKing.Visible = false;
            pcbWhiteWizard.Visible = false;
            pcbBlackRook.Visible = false;
            pcbBlackKnight.Visible = false;
            pcbBlackQueen.Visible = false;
            pcbBlackKing.Visible = false;
            pcbBlackWizard.Visible = false;

            if (!isPlacementPhase) return; // Hide sidebar after placement

            if (currentPlayer == "White")
            {
                pcbWhiteRook.Visible = true;
                pcbWhiteKnight.Visible = true;
                pcbWhiteQueen.Visible = true;
                pcbWhiteKing.Visible = true;
                pcbWhiteWizard.Visible = true;
            }
            else
            {
                pcbBlackRook.Visible = true;
                pcbBlackKnight.Visible = true;
                pcbBlackQueen.Visible = true;
                pcbBlackKing.Visible = true;
                pcbBlackWizard.Visible = true;
            }
        }

        // ====================== PLACEMENT PHASE ======================

        private void pcbSelectPiece_MouseDown(object sender, MouseEventArgs e)
        {
            if (!isPlacementPhase) return; // do nothing if no placement phase

            if (piecesPlaced >= 3)
            {
                MessageBox.Show("You can only place 3 pieces!");
                return;
            }

            PictureBox selectedPcb = (PictureBox)sender; // cast sender too picturebox (sender = object triggered event)

            if (currentlySelectedPiece != null)
                currentlySelectedPiece.BorderStyle = BorderStyle.None;

            selectedPcb.BorderStyle = BorderStyle.Fixed3D;
            currentlySelectedPiece = selectedPcb;

            string pieceType = GetPieceTypeFromSidebar(selectedPcb); //convert image into piece type
            string color = currentPlayer;   // "White" or "Black"

            lblStatus.Text = $"「{color.ToUpper()} {pieceType}」 SELECTED!";

            PlayCharacterVoiceLine(pieceType, color);

            if ((selectedPcb.Name.Contains("White") && currentPlayer != "White") || //show message if its not your turn
                (selectedPcb.Name.Contains("Black") && currentPlayer != "Black"))
            {
                MessageBox.Show($"It's {currentPlayer}'s turn to place!");
                currentlySelectedPiece = null;
                return;
            }

            currentPiece = new Piece(pieceType, currentPlayer); //create new game piece

            if (selectedPcb.Image != null) //if picture has an image, drag
            {
                selectedPcb.DoDragDrop(selectedPcb.Image, DragDropEffects.Copy);
            }
        }

        // ====================== GAME LOGIC ======================

        private void pcbAll_DragDrop(object sender, DragEventArgs e)
        {
            pcbTo = (PictureBox)sender; //square you dropped onto
            Image droppedImage = (Bitmap)e.Data.GetData(DataFormats.Bitmap); //get dragged image
            if (droppedImage == null) return;

            string targetTag = pcbTo.Tag?.ToString(); //get board position
            if (string.IsNullOrEmpty(targetTag) || targetTag.Length < 2) return;

            horizontal = int.Parse(targetTag.Substring(0, 1));
            vertical = int.Parse(targetTag.Substring(1, 1));

            // ====================== PLACEMENT PHASE ======================
            if (isPlacementPhase)
            {
                if (pcbTo.BackColor != Color.Green) return; //only allow green square
                if (currentlySelectedPiece == null)
                {
                    MessageBox.Show("Please select a piece from the sidebar first!");
                    return;
                }

                string pieceType = GetPieceTypeFromSidebar(currentlySelectedPiece);
                List<string> placedList = (currentPlayer == "White") ? whitePlacedPieces : blackPlacedPieces; //pick correct list based on player

                if (placedList.Contains(pieceType)) //prevent dupes
                {
                    MessageBox.Show($"You already placed your {pieceType}!");
                    return;
                }

                // place piece
                pcbTo.Image = currentlySelectedPiece.Image;
                currentPiece = new Piece(pieceType, currentPlayer);
                currentPiece.SetLocation(horizontal, vertical);

                string posKey = horizontal.ToString() + vertical.ToString(); //store piece
                boardPieces[posKey] = currentPiece;
                placedList.Add(pieceType); //mark piece as used
                DisableRemainingSidebarPiece();

                RefreshPieceBackgrounds(); //cleanup
                ClearBoardcolors();
                piecesPlaced++;

                if (piecesPlaced == 3)
                {
                    if (currentPlayer == "White")
                    {
                        currentPlayer = "Black";
                        piecesPlaced = 0;
                        lblStatus.Text = "Black - Place your 3 pieces on the top row";
                        radioBlack.Checked = true;
                        HighlightStartingRow();
                        UpdateSidebarVisibility();
                    }
                    else //placement done
                    {
                        isPlacementPhase = false;
                        currentPlayer = "White";
                        lblStatus.Text = "Placement finished! White starts moving.";
                        radioWhite.Checked = true;

                        ClearBoardcolors();
                        RefreshPieceBackgrounds();
                        UpdateSidebarVisibility();
                        StartNormalGame();
                    }
                }
                else
                {
                    HighlightStartingRow();
                }
                return;
            }
            // ====================== MOVING PHASE ======================
            if (pcbTo.BackColor != Color.Green)
            {
                MessageBox.Show("Invalid target square!");
                return;
            }

            if (currentPiece == null || currentPiece.Color != currentPlayer)
            {
                MessageBox.Show("Not your piece or invalid move!");
                return;
            }

            string oldKey = pcbFrom?.Tag?.ToString();
            string newKey = pcbTo.Tag?.ToString();

            if (string.IsNullOrEmpty(oldKey) || string.IsNullOrEmpty(newKey) || pcbFrom == pcbTo) //get position names
                return;

            bool isWizard = currentPiece.Name == "Wizard"; //is current piece wizard?

            if (isWizard && pcbTo.Image != null)
            {
                //check if piece is the same colour
                if (boardPieces.TryGetValue(newKey, out Piece targetPiece) &&
                    targetPiece.Color == currentPiece.Color)
                {
                    // 1. swap images on the board
                    Image tempImage = pcbTo.Image;
                    pcbTo.Image = pcbFrom.Image;
                    pcbFrom.Image = tempImage;

                    // 2. swap locations in the piece objects
                    int oldH = currentPiece.Horizontal;
                    int oldV = currentPiece.Vertical;
                    int newH = targetPiece.Horizontal;
                    int newV = targetPiece.Vertical;

                    currentPiece.SetLocation(newH, newV);
                    targetPiece.SetLocation(oldH, oldV);

                    // 3. swap in the game memory
                    boardPieces[oldKey] = targetPiece;
                    boardPieces[newKey] = currentPiece;
                }
                else
                {
                    // wizard lands on enemy
                    MessageBox.Show("Wizard cannot capture enemy pieces!");
                    return;
                }
            }
            else
            {
                // ================= NORMAL PIECE MOVE =================
                if (!isWizard && pcbTo.Image != null)
                {
                    MessageBox.Show("Invalid target square! (Cannot capture)");
                    return;
                }

                // normal move to empty square
                pcbTo.Image = pcbFrom.Image;
                pcbFrom.Image = null;

                // remove old position and place at new position
                boardPieces.Remove(oldKey);
                currentPiece.SetLocation(horizontal, vertical);
                boardPieces[newKey] = currentPiece;
            }

            // after successful move
            ClearBoardcolors();
            RefreshPieceBackgrounds();

            // switch turn
            currentPlayer = (currentPlayer == "White") ? "Black" : "White";
            CheckForWinner();
        }

        //====================== DRAGGING IMAGE ======================

        private void pcbAll_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap) && ((PictureBox)sender).BackColor == Color.Green)  //check if dragging image & is valid
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        // ====================== PIECE TYPE ======================

        private string GetPieceTypeFromSidebar(PictureBox pcb)
        {
            if (pcb == pcbWhiteRook || pcb == pcbBlackRook) return "Rook"; // check if picturebox is white or black rook
            if (pcb == pcbWhiteKnight || pcb == pcbBlackKnight) return "Knight";
            if (pcb == pcbWhiteQueen || pcb == pcbBlackQueen) return "Queen";
            if (pcb == pcbWhiteKing || pcb == pcbBlackKing) return "King";
            if (pcb == pcbWhiteWizard || pcb == pcbBlackWizard) return "Wizard";
            return ""; //unknown string if it doesnt match anything
        }

        //====================== HIGHLIGHT STARTING PLACES ======================

        private void HighlightStartingRow()
        {
            ClearBoardcolors();
            if (!isPlacementPhase) return; 

            int targetRow = (currentPlayer == "White") ? 1 : 3; //Detect which row

            foreach (PictureBox pb in gbxPlayingField.Controls.OfType<PictureBox>())
            {
                if (pb.Image != null) continue; //skip occupied place
                string tag = pb.Tag?.ToString(); //get position
                if (string.IsNullOrEmpty(tag) || tag.Length < 2) continue;

                int row = int.Parse(tag.Substring(1, 1)); //reads vertical coordinate
                if (row == targetRow) //highlight square
                    pb.BackColor = Color.Green;
            }
        }

        //====================== MANUALLY SWITCH RADIO BUTTON ======================

        private void radioPlayer_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (!rb.Checked || !isPlacementPhase) return;

            currentPlayer = (rb == radioWhite) ? "White" : "Black"; //set current player
            piecesPlaced = 0; //reset placement info
            whitePlacedPieces.Clear();
            blackPlacedPieces.Clear();
            lblStatus.Text = $"{currentPlayer} - Place your 3 pieces on the green row";

            HighlightStartingRow();
            UpdateSidebarVisibility();
        }

        //====================== START MAIN GAME PHASE ======================

        private void StartNormalGame()
        {
            lblStatus.Text = "White's turn - Click one of your pieces to move";
            ClearBoardcolors();
            RefreshPieceBackgrounds(); //adjust back color of piece
        }

        //====================== CHECK FOR A WINNER ======================

        private void CheckForWinner()
        {
            if (isPlacementPhase) return;
            string winner = null; //initialize winner variable

            for (int row = 1; row <= 3; row++) //check horizontal row and ignores starting row
            {
                var piecesInRow = new List<Piece>();
                foreach (var b in boardList)
                {
                    if (b.GetVertical() == row)
                    {
                        string key = b.GetHorizontal().ToString() + b.GetVertical().ToString();
                        if (boardPieces.TryGetValue(key, out Piece p) && p != null)
                        {
                            piecesInRow.Add(p);
                        }
                    }
                }

                if (piecesInRow.Count == 3) // if all 3 pieces are green, player wins
                {
                    string owner = piecesInRow[0].Color;
                    if ((owner == "White" && row == 1) || (owner == "Black" && row == 3))
                        continue;

                    if (piecesInRow.All(p => p.Color == owner))
                    {
                        winner = owner;
                        break;
                    }
                }
            }

            if (winner == null) //check columns for winner
            {
                for (int col = 1; col <= 3; col++)
                {
                    var piecesInCol = new List<Piece>();

                    foreach (var b in boardList)
                    {
                        if (b.GetHorizontal() == col)
                        {
                            string key = b.GetHorizontal().ToString() + b.GetVertical().ToString();
                            if (boardPieces.TryGetValue(key, out Piece p) && p != null)
                            {
                                piecesInCol.Add(p);
                            }
                        }
                    }

                    if (piecesInCol.Count == 3) //if pieces are same color, win
                    {
                        string owner = piecesInCol[0].Color;
                        if (piecesInCol.All(p => p.Color == owner))
                        {
                            winner = owner;
                            break;
                        }
                    }
                }
            }

            if (winner == null) // check diagonal for winner
            {
                var diag1 = new List<Piece>();

                foreach (var b in boardList)
                {
                    if (b.GetHorizontal() == b.GetVertical())
                    {
                        string key = b.GetHorizontal().ToString() + b.GetVertical().ToString();
                        if (boardPieces.TryGetValue(key, out Piece p) && p != null)
                        {
                            diag1.Add(p);
                        }
                    }
                }

                if (diag1.Count == 3)
                {
                    string owner = diag1[0].Color;
                    if (diag1.All(p => p.Color == owner))
                    {
                        winner = owner;
                    }
                }
            }

            if (winner == null) //check diagonal 2
            {
                var diag2 = new List<Piece>();

                foreach (var b in boardList)
                {
                    if (b.GetHorizontal() + b.GetVertical() == 4)
                    {
                        string key = b.GetHorizontal().ToString() + b.GetVertical().ToString();
                        if (boardPieces.TryGetValue(key, out Piece p) && p != null)
                        {
                            diag2.Add(p);
                        }
                    }
                }

                if (diag2.Count == 3)
                {
                    string owner = diag2[0].Color;
                    if (diag2.All(p => p.Color == owner))
                    {
                        winner = owner;
                    }
                }
            }

            if (winner != null) // announce winner
            {
                lblStatus.Text = $"{winner} wins! Three in a row!";
                MessageBox.Show($"{winner} has won the game!", "Game Over");

                ResetGame();
            }
            else
            {
                lblStatus.Text = $"{currentPlayer}'s turn - Click one of your pieces to move";
            }
        }

        //====================== UPDATE BACKGROUNDS ======================

        private void RefreshPieceBackgrounds()
        {
            foreach (PictureBox pb in gbxPlayingField.Controls.OfType<PictureBox>())
            {
                if (pb.Image == null) //reset empty squares
                {
                    pb.BackColor = Color.LightGray;
                    continue;
                }

                string key = pb.Tag?.ToString(); //update squares with pieces
                if (boardPieces.TryGetValue(key, out Piece p))
                {
                    pb.BackColor = (p.Color == "Black") ? Color.Black : Color.White;
                }
            }
        }

        //====================== CLEAR COLOURS ======================

        public void ClearBoardcolors()
        {
            foreach (PictureBox pb in gbxPlayingField.Controls.OfType<PictureBox>())
            {
                pb.BackColor = Color.LightGray;
            }
        }

        // ====================== MOVEMENT ======================
        private void pcbAll_MouseDown(object sender, MouseEventArgs e)
        {
            if (isPlacementPhase) return;

            pcbFrom = (PictureBox)sender;
            if (pcbFrom.Image == null) return;

            currentPiece = GetPieceAtLocation(pcbFrom);//look up ppiece in boardpieces using coordinates, store it in currentpiece
            if (currentPiece == null || currentPiece.Color != currentPlayer) //check if current player's piece
            {
                MessageBox.Show($"It's {currentPlayer}'s turn!");
                return;
            }

            GetBoardOptions(); //prepare move options
            UpdateBoardpieceLocations();

            pcbFrom.DoDragDrop(pcbFrom.Image, DragDropEffects.Copy); //initiate drag drop operation
        }

        // ====================== BOARD OPTIONS ======================

        public void GetBoardOptions()
        {
            pieceOptions = ""; //clear previous options
            if (currentPiece == null) return;

            foreach (Board b in boardList)
            {
                string option = currentPiece.GetMoveoptions(b.GetHorizontal(), b.GetVertical()); //call piece move logic
                if (!string.IsNullOrEmpty(option)) //store valid moves
                    pieceOptions += option;
            }
        }

        // ====================== HIGHLIGHT SQUARE WHERE PIECE CAN MOVE ======================

        public void UpdateBoardpieceLocations()
        {
            ClearBoardcolors(); 

            for (int i = 0; i < pieceOptions.Length; i += 2) //loop through piece options
            {
                string tag = pieceOptions.Substring(i, 2);
                foreach (PictureBox pb in gbxPlayingField.Controls.OfType<PictureBox>())
                {
                    bool isWizard = currentPiece.Name == "Wizard";

                    if (pb.Tag?.ToString() == tag && (pb.Image == null || isWizard))
                    {
                        pb.BackColor = Color.Green;
                        break;
                    }
                }
            }
        }

        private Piece GetPieceAtLocation(PictureBox pb)
        {
            if (pb == null || pb.Image == null) return null;
            string key = pb.Tag?.ToString();
            if (string.IsNullOrEmpty(key)) return null;
            boardPieces.TryGetValue(key, out Piece piece);
            return piece;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ResetGame();
        }

        private void ResetGame()
        {
            // 1. Clear all pieces from the board visually
            foreach (PictureBox pb in gbxPlayingField.Controls.OfType<PictureBox>())
            {
                pb.Image = null;
                pb.BackColor = Color.LightGray;
            }

            // 2. Clear all data structures
            boardPieces.Clear();
            whitePlacedPieces.Clear();
            blackPlacedPieces.Clear();

            // 3. Reset game state variables
            isPlacementPhase = true;
            currentPlayer = "White";
            piecesPlaced = 0;
            currentPiece = null;
            pcbFrom = null;
            pcbTo = null;
            currentlySelectedPiece = null;

            // 4. Reset radio buttons and sidebar
            radioWhite.Checked = true;
            radioBlack.Checked = false;

            // 5. Update UI elements
            lblStatus.Text = "White - Place your 3 pieces on the bottom row";

            // Hide all sidebar pieces initially, then show only White's
            UpdateSidebarVisibility();

            // 6. Highlight the starting row for White
            HighlightStartingRow();

            // Optional: Clear any remaining selection borders
            if (currentlySelectedPiece != null)
            {
                currentlySelectedPiece.BorderStyle = BorderStyle.None;
                currentlySelectedPiece = null;
            }
        }

        private void DisableRemainingSidebarPiece()
        {
            List<PictureBox> playerPieces = new List<PictureBox>();

            if (currentPlayer == "White")
            {
                playerPieces.Add(pcbWhiteRook);
                playerPieces.Add(pcbWhiteKnight);
                playerPieces.Add(pcbWhiteQueen);
                playerPieces.Add(pcbWhiteKing);
            }
            else
            {
                playerPieces.Add(pcbBlackRook);
                playerPieces.Add(pcbBlackKnight);
                playerPieces.Add(pcbBlackQueen);
                playerPieces.Add(pcbBlackKing);
            }

            List<string> placedList =
                (currentPlayer == "White") ? whitePlacedPieces : blackPlacedPieces;

            // If 3 pieces placed, disable the last one
            if (placedList.Count == 3)
            {
                foreach (PictureBox pb in playerPieces)
                {
                    string type = GetPieceTypeFromSidebar(pb);

                    if (!placedList.Contains(type))
                    {
                        pb.Enabled = false;
                        pb.BorderStyle = BorderStyle.None;
                    }
                }
            }
        }

        // ====================== ARDUINO CONNECTION ======================
        public void ArduinoConnected()
        {
            // This method is called when Arduino is successfully connected
            MessageBox.Show("✅ Arduino Connected Successfully!", "Success",
                           MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Optional: You can enable a "Run with Robot" button here later
            // btnRunArduino.Enabled = true;
        }

        private void btnRunArduino_Click_1(object sender, EventArgs e)
        {
            if (pcbFrom == null || pcbTo == null)
            {
                MessageBox.Show("Please make a move first by dragging a piece on the board!", "No move");
                return;
            }

            if (moveBusy)
            {
                MessageBox.Show("Robot is already moving!");
                return;
            }

            string fromTag = pcbFrom.Tag?.ToString();   // e.g. "11"
            string toTag = pcbTo.Tag?.ToString();       // e.g. "33"

            if (string.IsNullOrEmpty(fromTag) || string.IsNullOrEmpty(toTag))
                return;

            // Your tags are "horizontal vertical" → first digit = Col, second = Row
            int fromCol = int.Parse(fromTag[0].ToString());
            int fromRow = int.Parse(fromTag[1].ToString());
            int toCol = int.Parse(toTag[0].ToString());
            int toRow = int.Parse(toTag[1].ToString());

            currentFromLocation = locationList.FirstOrDefault(l => l.Row == fromRow && l.Col == fromCol);
            currentToLocation = locationList.FirstOrDefault(l => l.Row == toRow && l.Col == toCol);

            if (currentFromLocation == null || currentToLocation == null)
            {
                MessageBox.Show("Missing location data for this square.\nCheck locationList in Gameform_Load.", "Error");
                return;
            }

            moveArduinoCounter = 0;
            moveBusy = true;
            arduinoForm.Show();

            MessageBox.Show("Starting robot move...");
            NextRobotStep();
        }

        private void NextRobotStep()
        {
            if (!moveBusy || currentFromLocation == null || currentToLocation == null)
                return;

            string command = "";

            // Pickup phase
            if (moveArduinoCounter == 0)
                command = $"RS:{currentFromLocation.RS}";
            else if (moveArduinoCounter == 1)
                command = $"HS:{currentFromLocation.HS}";
            else if (moveArduinoCounter == 2)
                command = $"VS:{currentFromLocation.VS}";     // go down to piece
            else if (moveArduinoCounter == 3)
                command = "SS:1";                             // ← SUCTION ON to grab
            else if (moveArduinoCounter == 4)
                command = "VS:800";                           // lift up after grabbing (adjust if needed)

            // Move to target position
            else if (moveArduinoCounter == 5)
                command = $"RS:{currentToLocation.RS}";
            else if (moveArduinoCounter == 6)
                command = $"HS:{currentToLocation.HS}";
            else if (moveArduinoCounter == 7)
                command = $"VS:{currentToLocation.VS}";       // go down to drop position
            else if (moveArduinoCounter == 8)
                command = "SS:0";                             // ← SUCTION OFF to release
            else if (moveArduinoCounter == 9)
                command = "VS:0";                             // lift up after dropping

            else
            {
                // Finished
                moveBusy = false;
                moveArduinoCounter = 0;
                MessageBox.Show("Robot move completed successfully!", "Done");
                return;
            }

            arduinoForm.SendCommand(command);

            // Different delay times - bigger movements need more time
            int delayMs = 1600;
            if (moveArduinoCounter == 5 || moveArduinoCounter == 6)   // rotation + horizontal move
                delayMs = 2800;

            var timer = new System.Windows.Forms.Timer { Interval = delayMs };
            timer.Tick += (s, ev) =>
            {
                timer.Stop();
                moveArduinoCounter++;
                NextRobotStep();
            };
            timer.Start();
        }

        private void PlayCharacterVoiceLine(string pieceType, string color)
        {
            try
            {
                string soundFile = "";

                switch (pieceType.ToLower())
                {
                    case "rook":
                        soundFile = (color == "White")
                            ? @"C:\Users\otaku\OneDrive\Documentos\School\Sounds\kakyoin-thank-you.wav"
                            : @"C:\Users\otaku\OneDrive\Documentos\School\Sounds\jotaro-s-yare-yare-daze.wav";
                        break;

                    case "knight":
                        soundFile = (color == "White")
                            ? @"C:\Users\otaku\OneDrive\Documentos\School\Sounds\pucci-quote-3.wav"
                            : @"C:\Users\otaku\OneDrive\Documentos\School\Sounds\silver-chariot.wav";
                        break;
                        
                    case "queen":
                        soundFile = (color == "White")
                            ? @"C:\Users\otaku\OneDrive\Documentos\School\Sounds\kira-yoshikage-s-theme.wav"
                            : @"C:\Users\otaku\OneDrive\Documentos\School\Sounds\jolyne.wav";
                        break;

                    case "king":
                        soundFile = (color == "White")
                            ? @"C:\Users\otaku\OneDrive\Documentos\School\Sounds\dio.wav"
                            : @"C:\Users\otaku\OneDrive\Documentos\School\Sounds\diavolo-ringtone.wav";
                        break;

                    case "wizard":
                        soundFile = (color == "White")
                            ? @"C:\Users\otaku\OneDrive\Documentos\School\Sounds\joseph-joestar-oh-shit.wav"
                            : @"C:\Users\otaku\OneDrive\Documentos\School\Sounds\avdol-jojo-yes-i-am-yes.wav";
                        break;

                    default:
                        soundFile = @"C:\Sounds\JoJo\default-yare-yare.wav";
                        break;
                }

                soundPlayer.SoundLocation = soundFile;
                soundPlayer.Play();
            }
            catch
            {
                // Silently fail if sound is missing
                lblStatus.Text += " (Voice line missing)";
            }
        }
    }
}