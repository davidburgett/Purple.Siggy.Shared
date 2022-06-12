namespace PurpleSiggy.Games {
    public enum Directions { Up, Right, Down, Left };
    public class AvailableDirections {
        public bool Up { get; set; }
        public bool Right { get; set; }
        public bool Down { get; set; }
        public bool Left { get; set; }
        public bool GetDirectionAvailability(Directions Direction) {
            return Direction switch {
                Directions.Up => this.Up,
                Directions.Right => this.Right,
                Directions.Down => this.Down,
                Directions.Left => this.Left,
                _ => false
            };
        }
    }
    public enum GridOptions {
        MoveDistance = 3,
        DestroySimilarPiecesInSpecificDirections = 4,
        DestroyDissimilarPiecesInSpecificDirections = 5,
        ConvertPiecesInSpecificDirection = 6,
        DestroySimilarAdjacentPieces = 7,
        DestroyDissimilarAdjacentPieces = 8,
        TiltBoard = 9
    }
    public class WinCondition {
        public enum WinConditionTypes { LineOfAnyTypeAtLeastThisLong = 2, LineOfSpecificTypeAtLeastThisLong = 3, LineOfSpecificTypeExactlyThisLong = 4 }
        public string[] WinConditionTypeDescriptions =
        {
        "At least {SpecificValue1} of any item in a row",
        "At least {SpecificValue1} {CellValue} in a row",
        "Exactly {SpecificValue1} {CellValue} in a row"

    };
        public int CellValue { get; init; }
        public WinConditionTypes WinConditionType { get; init; }
        public int SpecificValue1 { get; init; } = -1;
        public int SpecificValue2 { get; init; } = -1;
        //public WinCondition(int CellValue, WinConditionTypes WinConditionType) {
        //    this.CellValue = CellValue;
        //    this.WinConditionType = WinConditionType;
        //}
        //public WinCondition(int CellValue, WinConditionTypes WinConditionType, int SpecificValue1) {
        //    this.CellValue = CellValue;
        //    this.WinConditionType = WinConditionType;
        //    this.SpecificValue1 = SpecificValue1;
        //}
        //public WinCondition(int CellValue, WinConditionTypes WinConditionType, int SpecificValue1, int SpecificValue2) {
        //    this.CellValue = CellValue;
        //    this.WinConditionType = WinConditionType;
        //    this.SpecificValue1 = SpecificValue1;
        //    this.SpecificValue2 = SpecificValue2;
        //}
        public WinCondition(WinConditionTypes WinConditionType, int SpecificValue1, int SpecificValue2) {
            this.WinConditionType = WinConditionType;
            this.SpecificValue1 = SpecificValue1;
            this.SpecificValue2 = SpecificValue2;
        }
        public WinCondition(OneShot.Data.WinConditionSpecificValue winConditionValues) {
            this.WinConditionType = (WinConditionTypes)winConditionValues.WinConditionId;
            this.SpecificValue1 = int.Parse(winConditionValues.SpecificValue1);
            this.SpecificValue2 = int.Parse(winConditionValues.SpecificValue2 ?? "-1");
        }
        public string Description { get => WinConditionTypeDescriptions[(int)WinConditionType].Replace("{CellValue}", "<img class='icon' src='_content/PurpleSiggy.Games.OneShot/images/" + CellValue.ToString() + ".png'/>").Replace("{SpecificValue1}", SpecificValue1.ToString()).Replace("{SpecificValue2}", SpecificValue2.ToString()); }
        public bool IsMet(Grid GridToCheck, int Location) {
            bool isMet = false;
            switch (this.WinConditionType) {
                case WinConditionTypes.LineOfAnyTypeAtLeastThisLong:
                    var targetValue = GridToCheck[Location];
                    var length = 1;
                    var location = GridToCheck.GetAdjacentLocation(Location, Directions.Left);
                    if (location > 0) {
                        while (GridToCheck[location] == targetValue) {
                            length++;
                            if (length >= this.SpecificValue1)
                                return true;
                            location = GridToCheck.GetAdjacentLocation(Location, Directions.Left);
                            if (location < 0)
                                break;
                        }
                    }
                    if (length >= this.SpecificValue1)
                        return true;
                    location = GridToCheck.GetAdjacentLocation(Location, Directions.Right);
                    if (location > 0) {
                        while (GridToCheck[location] == targetValue) {
                            length++;
                            if (length >= this.SpecificValue1)
                                return true;
                            location = GridToCheck.GetAdjacentLocation(Location, Directions.Right);
                            if (location < 0)
                                break;
                        }
                    }
                    return (length >= this.SpecificValue1);
                    //for (int i = 0; i > this.SpecificValue1; i++) {
                    //    location = GridToCheck.GetAdjacentLocation(location, Directions.Left);
                    //    if (location > -1 && GridToCheck[location] == this.SpecificValue1) {
                    //        length++;
                    //    }
                    //    else {
                    //        break;
                    //    }
                    //}
                    //location = Location;
                    //for (int i = 0; i > this.SpecificValue1; i++) {
                    //    location = GridToCheck.GetAdjacentLocation(location, Directions.Right);
                    //    if (location > -1 && GridToCheck[location] == this.SpecificValue1) {
                    //        length++;
                    //    }
                    //    else {
                    //        break;
                    //    }
                    //}
                    //if (length > this.SpecificValue1)
                    //    return true;

                    //length = 1;
                    //location = Location;
                    //for (int i = 0; i > this.SpecificValue1; i++) {
                    //    location = GridToCheck.GetAdjacentLocation(location, Directions.Up);
                    //    if (location>-1 &&  GridToCheck[location] == this.SpecificValue1) {
                    //        length++;
                    //    }
                    //    else {
                    //        break;
                    //    }
                    //}
                    //location = Location;
                    //for (int i = 0; i > this.SpecificValue1; i++) {
                    //    location = GridToCheck.GetAdjacentLocation(location, Directions.Down);
                    //    if (location > -1 && GridToCheck[location] == this.SpecificValue1) {
                    //        length++;
                    //    }
                    //    else {
                    //        break;
                    //    }
                    //}
                    //return length >= this.SpecificValue1;


                    //    if (length > this.SpecificValue1) {
                    //    var cellsInRow = GridToCheck.FindAdjacentCellsInSameRow(Location, this.SpecificValue1);
                    //var rowCheck = false;
                    //if (cellsInRow[0]==this.CellValue
                    //cellsInRow.ForEach(cell => { });
                    //int longestLine = 0;
                    //int currentCellValue = -1;
                    //int r = (int)Math.Floor((decimal)Location / GridToCheck.Size);
                    //for (int i = 0; i < GridToCheck.Size; i++) {
                    //    if (GridToCheck[r+i] == currentCellValue) {
                    //        longestLine++;
                    //    }
                    //    else {
                    //        if (GridToCheck[r+i] > 0) {
                    //            currentCellValue = GridToCheck[r+i];
                    //            longestLine = 1;
                    //        }
                    //        else {
                    //            currentCellValue = -1;
                    //            longestLine = 0;
                    //        }
                    //    }
                    //}
                    //if (longestLine > this.SpecificValue1)
                    //    return true;
                    //int c=Location % GridToCheck.Size;
                    //for (int i = c; i < GridToCheck.Values.Count; i+=GridToCheck.Size) {
                    //    if (GridToCheck[i] == currentCellValue) {
                    //        longestLine++;
                    //    }
                    //    else {
                    //        if (GridToCheck[i] > 0) {
                    //            currentCellValue = GridToCheck[i];
                    //            longestLine = 1;
                    //        }
                    //        else {
                    //            currentCellValue = -1;
                    //            longestLine = 0;
                    //        }
                    //    }
                    //}
                    //return longestLine > this.SpecificValue1;
                    for (int i = 0; i < GridToCheck.LocationValues.Count; i++) {
                        if (GridToCheck.LocationValues[i] > 0) {
                            if (CheckAdjacentCellForSpecificValue(GridToCheck, i, Directions.Right, this.SpecificValue1, GridToCheck.LocationValues[i]))
                                isMet = true;
                            if (CheckAdjacentCellForSpecificValue(GridToCheck, i, Directions.Down, this.SpecificValue1, GridToCheck.LocationValues[i]))
                                isMet = true;
                        }
                    }
                    break;
                case WinConditionTypes.LineOfSpecificTypeAtLeastThisLong:
                    for (int i = 0; i < GridToCheck.LocationValues.Count; i++) {
                        if (GridToCheck.LocationValues[i] > 0) {
                            if (CheckAdjacentCellForSpecificValue(GridToCheck, i, Directions.Right, this.SpecificValue1, this.CellValue))
                                isMet = true;
                            if (CheckAdjacentCellForSpecificValue(GridToCheck, i, Directions.Down, this.SpecificValue1, this.CellValue))
                                isMet = true;
                        }
                    }
                    //for (int r = 0; r < GridToCheck.Size; r++)
                    //    for (int c = 0; c <= GridToCheck.Size - this.SpecificValue1; c++)
                    //       if (CheckAdjacentCellForSpecificValue(GridToCheck,r*GridToCheck.Size+c,Directions.Right,this.SpecificValue1,GridToCheck.Values[r*GridToCheck.Size+c]))
                    //                isMet = true;
                    //        if (GridToCheck.Values[r * GridToCheck.Size + c] == this.CellValue)
                    //        {
                    //            //Check line to the left
                    //            for (int i = 1; i < this.SpecificValue1; i++)
                    //                if (GridToCheck.Values[r * GridToCheck.Size + c + i] != this.CellValue)
                    //                    break;
                    //        }
                    //for (int r = 0; r <= GridToCheck.Size - this.SpecificValue1; r++)
                    //    for (int c = 0; c < GridToCheck.Size; c++)
                    //        if (GridToCheck.Values[r * GridToCheck.Size + c] == this.CellValue)
                    //        {
                    //            //Check line to the left
                    //            for (int i = r + GridToCheck.Size; i < GridToCheck.Values.Count; i += GridToCheck.Size)
                    //                if (GridToCheck.Values[r * GridToCheck.Size + c + i] != this.CellValue)
                    //                    break;
                    //            return true;
                    //        }
                    break;
                case WinConditionTypes.LineOfSpecificTypeExactlyThisLong:
                    break;
            }
            return isMet;
        }
        public bool IsMet(Grid GridToCheck) {
            bool isMet = false;
            switch (this.WinConditionType) {
                case WinConditionTypes.LineOfAnyTypeAtLeastThisLong:
                    for (int i = 0; i < GridToCheck.LocationValues.Count(); i++)
                        if (GridToCheck.LocationValues[i] > 0) {
                            if (CheckAdjacentCellForSpecificValue(GridToCheck, i, Directions.Right, this.SpecificValue1, GridToCheck.LocationValues[i]) ||
                                CheckAdjacentCellForSpecificValue(GridToCheck, i, Directions.Down, this.SpecificValue1, GridToCheck.LocationValues[i]))
                                return true;
                        }
                    return false;

                    break;
                case WinConditionTypes.LineOfSpecificTypeAtLeastThisLong:
                    for (int i = 0; i < GridToCheck.LocationValues.Count; i++) {
                        if (GridToCheck.LocationValues[i] > 0) {
                            if (CheckAdjacentCellForSpecificValue(GridToCheck, i, Directions.Right, this.SpecificValue1, this.CellValue))
                                isMet = true;
                            if (CheckAdjacentCellForSpecificValue(GridToCheck, i, Directions.Down, this.SpecificValue1, this.CellValue))
                                isMet = true;
                        }
                    }
                    //for (int r = 0; r < GridToCheck.Size; r++)
                    //    for (int c = 0; c <= GridToCheck.Size - this.SpecificValue1; c++)
                    //       if (CheckAdjacentCellForSpecificValue(GridToCheck,r*GridToCheck.Size+c,Directions.Right,this.SpecificValue1,GridToCheck.Values[r*GridToCheck.Size+c]))
                    //                isMet = true;
                    //        if (GridToCheck.Values[r * GridToCheck.Size + c] == this.CellValue)
                    //        {
                    //            //Check line to the left
                    //            for (int i = 1; i < this.SpecificValue1; i++)
                    //                if (GridToCheck.Values[r * GridToCheck.Size + c + i] != this.CellValue)
                    //                    break;
                    //        }
                    //for (int r = 0; r <= GridToCheck.Size - this.SpecificValue1; r++)
                    //    for (int c = 0; c < GridToCheck.Size; c++)
                    //        if (GridToCheck.Values[r * GridToCheck.Size + c] == this.CellValue)
                    //        {
                    //            //Check line to the left
                    //            for (int i = r + GridToCheck.Size; i < GridToCheck.Values.Count; i += GridToCheck.Size)
                    //                if (GridToCheck.Values[r * GridToCheck.Size + c + i] != this.CellValue)
                    //                    break;
                    //            return true;
                    //        }
                    break;
                case WinConditionTypes.LineOfSpecificTypeExactlyThisLong:
                    break;
            }
            return isMet;
        }
        private bool CheckAdjacentCellForSpecificValue(Grid GridToCheck, int StartingLocation, Directions Direction, int MaxNumberToCheck, int ExpectedValue) {
            if (StartingLocation < 0 || StartingLocation >= GridToCheck.LocationValues.Count || MaxNumberToCheck > GridToCheck.Size) return false;
            switch (Direction) {
                case Directions.Up:
                    if (StartingLocation < GridToCheck.Size * MaxNumberToCheck)
                        return false;
                    break;
                case Directions.Right:
                    if (StartingLocation % GridToCheck.Size > GridToCheck.Size - MaxNumberToCheck)
                        return false;
                    break;
                case Directions.Down:
                    if (StartingLocation >= GridToCheck.LocationValues.Count - (GridToCheck.Size * (MaxNumberToCheck - 1)))
                        return false;
                    break;
                case Directions.Left:
                    if (StartingLocation % GridToCheck.Size < GridToCheck.Size - MaxNumberToCheck)
                        return false;
                    break;

            }
            int currentLocation = StartingLocation;
            for (int i = 0; i < MaxNumberToCheck; i++) {
                if (currentLocation < 0 || GridToCheck.LocationValues[currentLocation] != ExpectedValue)
                    return false;
                currentLocation = GridToCheck.GetAdjacentLocation(currentLocation, Direction);
            }
            return true;
        }
    }

    public class Options {
        public int MoveDistance { get; set; } = 1;
        public Directions? TiltBoard { get; set; }

    }
    public enum PieceConditions { Normal, DestroyAdjacentPieces, ConvertAdjacentPieces };
    public class PieceCondition {
        public PieceConditions Condition { get; set; } = PieceConditions.Normal;
        public Directions? Directions { get; set; } = null;
        public int SpecificValue1 { get; set; } = -1;
        public int SpecificValue2 { get; set; } = -1;
    }
    public class Piece {
        public int Value { get; set; }
        public PieceCondition Condition { get; set; } = null;
    }
    public record Move {
        public int Location { get; init; } = -1;
        public Directions Direction { get; init; } = Directions.Up;
        public int Destination { get; init; } = -1;
        public Move(int location, Directions direction, int destination) {
            this.Location = location;
            this.Direction = direction;
            this.Destination = destination;
        }
    }
    public class Grid {
        //Location Values
        // -1 = Unavailable
        //  0 = Empty
        //  1 = Occupied

        public int GetAdjacentLocationInit(int StartingLocation, Directions Direction) {

            if (StartingLocation < 0 || StartingLocation > LocationValues.Count)
                return -1;
            int destination = Direction switch {
                Directions.Up => StartingLocation - Size,
                Directions.Right => StartingLocation + 1,
                Directions.Down => StartingLocation + Size,
                Directions.Left => StartingLocation - 1,
                _ => -1
            };
            if ((destination < 0) ||
                (destination >= _Values.Count) ||
                (Direction == Directions.Right && destination % Size == 0) ||
                (Direction == Directions.Left && destination % Size == Size - 1) ||
                (LocationValues[destination] == -1)) return -1;
            return destination;
        }

        public int GetAdjacentLocation(int StartingLocation, Directions Direction) {
            return Direction switch {
                Directions.Up => _ValuesUp[StartingLocation],
                Directions.Right => _ValuesRight[StartingLocation],
                Directions.Down => _ValuesDown[StartingLocation],
                Directions.Left => _ValuesLeft[StartingLocation],
                _ => -1
            };
        }
        List<int> _Values = new();
        List<int> _ValuesUp = new();
        List<int> _ValuesRight = new();
        List<int> _ValuesDown = new();
        List<int> _ValuesLeft = new();

        public List<WinCondition> WinConditions { get; set; } = new();
        int _size = 0;

        public List<int> LocationValues { get { return _Values; } }
        public int Size {
            get {
                if (_size == 0) _size = (int)Math.Sqrt(_Values.Count);
                return _size;
            }
        }
        public int Distance { get; set; } = 1;
        public Options Options { get; set; } = new();
        public void SetOption(int OptionID, string Value) {
            switch ((GridOptions)OptionID) {
                case GridOptions.MoveDistance:
                    this.Options.MoveDistance = int.Parse(Value);
                    break;
                case GridOptions.DestroyDissimilarPiecesInSpecificDirections:
                    //TODO
                    break;
                case GridOptions.ConvertPiecesInSpecificDirection:
                    //TODO
                    break;
                case GridOptions.DestroyDissimilarAdjacentPieces:
                    //TODO
                    break;
                case GridOptions.DestroySimilarAdjacentPieces:
                    //TODO
                    break;
                case GridOptions.TiltBoard:
                    this.Options.TiltBoard = (Directions)int.Parse(Value);
                    //TODO
                    break;
            }
        }

        public Grid Initialize(params int[] Values) {
            if (Math.Sqrt(Values.Length) % 1 != 0)
                throw new ArgumentException("Must provide a full grid of values. Did you intend to use InitializeSpecificValues?");
            _Values.Clear();
            _Values.AddRange(Values);
            _size = (int)Math.Sqrt(Values.Length);
            _ValuesUp.Clear();
            _ValuesRight.Clear();
            _ValuesDown.Clear();
            _ValuesLeft.Clear();
            for (int i = 0; i < Values.Length; i++) {
                _ValuesUp.Add(GetAdjacentLocationInit(i, Directions.Up));
                _ValuesRight.Add(GetAdjacentLocationInit(i, Directions.Right));
                _ValuesDown.Add(GetAdjacentLocationInit(i, Directions.Down));
                _ValuesLeft.Add(GetAdjacentLocationInit(i, Directions.Left));
            }
            return this;
        }
        public Grid Initialize(params string[] Values) {
            if (Math.Sqrt(Values.Length) % 1 != 0)
                throw new ArgumentException("Must provide a full grid of values. Did you intend to use InitializeSpecificValues?");
            _Values.Clear();
            foreach (string value in Values)
                _Values.Add(value == "." ? -1 : int.Parse(value));
            _size = (int)Math.Sqrt(Values.Length);
            _ValuesUp.Clear();
            _ValuesRight.Clear();
            _ValuesDown.Clear();
            _ValuesLeft.Clear();
            for (int i = 0; i < Values.Length; i++) {
                _ValuesUp.Add(GetAdjacentLocationInit(i, Directions.Up));
                _ValuesRight.Add(GetAdjacentLocationInit(i, Directions.Right));
                _ValuesDown.Add(GetAdjacentLocationInit(i, Directions.Down));
                _ValuesLeft.Add(GetAdjacentLocationInit(i, Directions.Left));
            }
            return this;
        }
        public Grid InitializeSpecificValues(int TotalNumberOfValues, params int[] Values) {
            if (Math.Sqrt(TotalNumberOfValues) % 1 != 0)
                throw new ArgumentException("Must specify a perfect square Total Number of Values.");
            _Values.Clear();
            _Values = Enumerable.Repeat(0, TotalNumberOfValues).ToList();
            for (int i = 0; i < Values.Length; i += 2)
                _Values[i] = Values[i + 1];
            _size = (int)Math.Sqrt(TotalNumberOfValues);
            return this;
        }
        public Grid AddWinCondition(int WinConditionID, string SpecificValue1, string SpecificValue2) {
            int sv2 = -1;
            int.TryParse(SpecificValue2, out sv2);
            WinConditions.Add(new WinCondition((WinCondition.WinConditionTypes)WinConditionID, int.Parse(SpecificValue1), sv2));
            return this;
        }

        public Grid AddWinCondition(WinCondition Condition) {
            WinConditions.Add(Condition);
            return this;
        }
        public int this[int index] {
            get => _Values.ElementAt(index);
        }
        public bool AllWinConditionsAreMet() => this.WinConditions.All(wc => wc.IsMet(this));
        public bool AllWinConditionsAreMet(int LocationInQuestion) => this.WinConditions.All(wc => wc.IsMet(this, LocationInQuestion));

        public List<Move> GetMoves() {
            List<Move> moves = new List<Move>();
            int destination = -1, moveCount = 0;
            for (int origin = 0; origin < _Values.Count; origin++) {
                if (_Values[origin] > 0) {
                    moveCount = 0;
                    destination = origin;
                    do {
                        destination = GetAdjacentLocation(destination, Directions.Up);
                        moveCount++;
                    } while (destination > -1 && destination < _Values.Count && _Values[destination] == 0 && moveCount < Distance);
                    if (_Values[origin] > 0 && destination > -1 && destination < _Values.Count && _Values[destination] == 0)
                        moves.Add(new Move(origin, Directions.Up, destination));
                    moveCount = 0;
                    destination = origin;
                    do {
                        destination = GetAdjacentLocation(destination, Directions.Right);
                        moveCount++;
                    } while (destination > -1 && destination < _Values.Count && _Values[destination] == 0 && moveCount < Distance);
                    if (_Values[origin] > 0 && destination > -1 && destination < _Values.Count && _Values[destination] == 0)
                        moves.Add(new Move(origin, Directions.Right, destination));
                    moveCount = 0;
                    destination = origin;
                    do {
                        destination = GetAdjacentLocation(destination, Directions.Down);
                        moveCount++;
                    } while (destination > -1 && destination < _Values.Count && _Values[destination] == 0 && moveCount < Distance);
                    if (_Values[origin] > 0 && destination > -1 && destination < _Values.Count && _Values[destination] == 0)
                        moves.Add(new Move(origin, Directions.Down, destination));
                    moveCount = 0;
                    destination = origin;
                    do {
                        destination = GetAdjacentLocation(destination, Directions.Left);
                        moveCount++;
                    } while (destination > -1 && destination < _Values.Count && _Values[destination] == 0 && moveCount < Distance);
                    if (_Values[origin] > 0 && destination > -1 && destination < _Values.Count && _Values[destination] == 0)
                        moves.Add(new Move(origin, Directions.Left, destination));
                }
            }
            return moves;
        }
        public bool CanMove(int StartingLocation, Directions Direction) {
            if (_Values[StartingLocation] <= 0)
                return false;
            int destination = GetAdjacentLocation(StartingLocation, Direction);
            if (destination < 0 || destination >= LocationValues.Count)
                return false;
            return _Values.ElementAt(destination) >= 0;
        }

        public int Move(int StartingLocation, Directions Direction) {
            if (!CanMove(StartingLocation, Direction)) return -1;
            int destination = StartingLocation;
            int temp = GetAdjacentLocation(destination, Direction);
            int moveCount = 0;
            while (destination != temp && temp > -1 && moveCount < Options.MoveDistance) {
                if (_Values[temp] == 0) {
                    destination = temp;
                }
                temp = GetAdjacentLocation(destination, Direction);
                moveCount++;
            }
            if (destination != StartingLocation) {
                _Values[destination] = _Values.ElementAt(StartingLocation);
                _Values[StartingLocation] = 0;
            }

            //for (int i = 1; i <= Options.MoveDistance; i++) {
            //    temp = Direction switch {
            //        Directions.Up => StartingLocation - Size,
            //        Directions.Right => StartingLocation + 1,
            //        Directions.Down => StartingLocation + Size,
            //        Directions.Left => StartingLocation - 1,
            //        _ => -1
            //    };
            //    if (_Values[temp] == 0) {
            //        destination = t;
            //        _Values[destination] = _Values.ElementAt(StartingLocation);
            //        _Values[StartingLocation] = 0;
            //    }
            //    else {
            //        return -1;
            //    }
            //}
            return destination == StartingLocation ? -1 : destination;
            //   CheckForEffects(StartingLocation);
        }
        public int Move(Move move) {
            if (!CanMove(move.Location, move.Direction)) return -1;
            int destination = move.Location;
            int temp = GetAdjacentLocation(destination, move.Direction);
            int moveCount = 0;
            while (destination != temp && temp > -1 && moveCount < Options.MoveDistance) {
                if (_Values[temp] == 0) {
                    destination = temp;
                }
                temp = GetAdjacentLocation(destination, move.Direction);
                moveCount++;
            }
            if (destination != move.Location) {
                _Values[destination] = _Values.ElementAt(move.Location);
                _Values[move.Location] = 0;
            }

            return destination == move.Location ? -1 : destination;
            //   CheckForEffects(StartingLocation);
        }
        public List<int> FindAdjacentCellsInSameRow(int StartingLocation, int MaxDistance) {
            List<int> result = new List<int>();
            int r = (int)Math.Floor((decimal)StartingLocation / LocationValues.Count);
            for (int i = Math.Max(r * Size, StartingLocation - MaxDistance); i < Math.Min(r * (Size + 1) - 1, StartingLocation + MaxDistance); i++)
                result.Add(i);
            return result;
        }
        public List<int> FindAdjacentCellsInSameColumn(int StartingLocation, int MaxDistance) {
            List<int> result = new List<int>();
            int c = StartingLocation % Size;
            for (int i = c; i <= LocationValues.Count; i += Size)
                if (i >= StartingLocation - (MaxDistance * Size) && i <= StartingLocation + (MaxDistance * Size))
                    result.Add(i);
            return result;
        }

        // private void CheckForEffects(int StartingLocation)

    }
}