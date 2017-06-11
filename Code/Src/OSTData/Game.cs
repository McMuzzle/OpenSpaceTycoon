namespace OSTData {

    /// <summary>
    /// Classe representant une partie de Open space tycoon
    /// </summary>
    public class Game {
        private Universe _Universe = null;

        /// <summary>
        /// new game creation
        /// </summary>
        public Game() {
            _Universe = new Universe(0);
            foreach (Station s in _Universe.GetStations()) {
                s.InitProduct();
            }
        }

        /// <summary>Get the universe in this game </summary>
        public Universe Universe {
            get { return _Universe; }
        }

        /// <summary> Take on step in the game (1/5 of a day) </summary>
        public void Update() {
            _Universe.Update();
        }
    }
}