// Copyright 2023 ReWaffle LLC. All rights reserved.


namespace Naninovel.UI
{
    public class ControlPanelTitleButton : ScriptableButton
    {
        [ManagedText("DefaultUI")]
        protected static string ConfirmationMessage = "タイトル画面に戻りますか? \n セーブしていないデータは当然残りません。\n セーブしてても残らんかも(デバッグ不足)";

        private IStateManager gameState;
        private IUIManager uiManager;
        private IConfirmationUI confirmationUI;

        protected override void Awake ()
        {
            base.Awake();

            gameState = Engine.GetService<IStateManager>();
            uiManager = Engine.GetService<IUIManager>();
        }

        protected override void Start ()
        {
            base.Start();

            confirmationUI = uiManager.GetUI<IConfirmationUI>();
        }

        protected override void OnButtonClick ()
        {
            uiManager.GetUI<IPauseUI>()?.Hide();

            ExitToTitleAsync();
        }

        private async void ExitToTitleAsync ()
        {
            if (!await confirmationUI.ConfirmAsync(ConfirmationMessage)) return;

            await gameState.ResetStateAsync();
            uiManager.GetUI<ITitleUI>()?.Show();
        }
    } 
}
