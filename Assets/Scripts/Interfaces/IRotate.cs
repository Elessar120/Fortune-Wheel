  using System;

  public interface IRotate
    {
        public void Rotate();
        public Action OnSpinEnd { get; set; }
        public  Action OnLoseOneSpin{ get; set; }// can fill with things like particles
        public  Action OnWinGame{ get; set; }
        public  Action OnExtraChance{ get; set; }// can fill with things like particles
        public  Action OnUpdateUI{ get; set; }
        public  Action OnLoseGame{ get; set; }
        public  Action OnStartGame{ get; set; }
        public  Action<bool> OnSaveGameResult{ get; set; }
    }