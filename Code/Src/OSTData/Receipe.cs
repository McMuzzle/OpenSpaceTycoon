using System;
using System.Collections.Generic;

namespace OSTData {

    /// <summary>
    /// Classe representant une recette de production
    /// </summary>
    [Serializable]
    public class Receipe {

        /// <summary>
        /// Constructeur de base
        /// </summary>
        /// <param name="maxFreq"> Le nombre de fois que la recette peut etre effectue par jour</param>
        public Receipe(int maxFreq) {
            MaxFreq = maxFreq;
        }

        /// <summary>
        /// Ajouter un parametre d'entree de cette recette. C'est a dire une qantite d'un certain type de ressource qui doit etre present pour que
        /// la recette puisse etre lance
        /// </summary>
        /// <param name="type">type de la ressource</param>
        /// <param name="qte">qte necessaire</param>
        public void AddInput(ResourceElement.ResourceType type, int qte) {
            if (!_inputs.ContainsKey(type))
                _inputs.Add(type, 0);
            _inputs[type] = qte;
        }

        /// <summary>
        /// recuperer les besoins totaux pour un type de ressource pour cette recette
        /// </summary>
        /// <param name="type">le type de ressource demande</param>
        /// <returns>la quantite necessaire</returns>
        public int GetResourceNeed(ResourceElement.ResourceType type) {
            if (_inputs.ContainsKey(type)) {
                return _inputs[type];
            }
            return 0;
        }

        /// <summary>
        /// Ajouter un output a cette recette. C'est a dire qu'une fois complete c'est qu'une quantite de ressource sera produite
        /// </summary>
        /// <param name="type">le type de ressource a produire</param>
        /// <param name="qte">la qte de la ressource produite</param>
        public void AddOutput(ResourceElement.ResourceType type, int qte) {
            if (!_outputs.ContainsKey(type))
                _outputs.Add(type, 0);
            _outputs[type] = qte;
        }

        /// <summary>
        /// Demande a la recette de s'executer pour une station donnee
        /// </summary>
        /// <param name="station"></param>
        /// <param name="timestamp">l'heure en cours</param>
        public bool ProduceOneBatch(Station station, int timestamp) {
            Hangar homeHangar = station.GetHangar(-1);

            foreach (ResourceElement.ResourceType e in _inputs.Keys) {
                if (homeHangar.GetResourceQte(e) < _inputs[e])
                    return false;
            }
            //toutes les inputs sont presentes
            foreach (ResourceElement.ResourceType e in _inputs.Keys) {
                ResourceStack stack = homeHangar.GetStack(e, _inputs[e]);
            }

            foreach (ResourceElement.ResourceType e in _outputs.Keys) {
                //trouver tout les gens qui ont un standing
                HashSet<int> withStanding = station.GetCorpWithStanding(e);
                int qteToProd = _outputs[e];
                foreach (int i in withStanding) {
                    Hangar hisHangar = station.GetHangar(i);
                    if (null != hisHangar) {
                        ResourceElement elem = new ResourceElement(e, station, qteToProd, timestamp);
                        ResourceStack stack = new ResourceStack(elem);
                        hisHangar.Add(stack);
                    }
                }
            }
            return true;
        }

        /// <summary> Le nombre de fois maximum que peut etre effectue la recette par jour. </summary>
        public int MaxFreq { get; private set; }

        /// <summary>
        /// Permet de connaitre si des outputs de cette recette produise un certain type de ressource
        /// </summary>
        /// <param name="type"> La ressource a tester</param>
        /// <returns> true si la recette peut produire ce type de ressource </returns>
        public bool IsProducing(ResourceElement.ResourceType type) {
            return _outputs.ContainsKey(type);
        }

        [Newtonsoft.Json.JsonProperty]
        private Dictionary<ResourceElement.ResourceType, int> _inputs = new Dictionary<ResourceElement.ResourceType, int>();

        [Newtonsoft.Json.JsonProperty]
        private Dictionary<ResourceElement.ResourceType, int> _outputs = new Dictionary<ResourceElement.ResourceType, int>();
    }
}