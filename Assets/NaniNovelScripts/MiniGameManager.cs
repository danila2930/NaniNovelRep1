using Naninovel;
using Naninovel.Commands;
using CysharpTask = Cysharp.Threading.Tasks.UniTask;
using UnityEngine;

[CommandAlias("startMemoryGame")]
public class StartMemoryGameCommand : Command
{
    public override async UniTask ExecuteAsync (AsyncToken asyncToken)
    {
        
        DTT.MinigameMemory.MemoryGameManager miniGameManager = Object.FindObjectOfType<DTT.MinigameMemory.MemoryGameManager>();
        DTT.MinigameMemory.MemoryGameSettings Settings = Resources.Load<DTT.MinigameMemory.MemoryGameSettings>("StartLocLevel");
        Canvas MiniGameCanvas = GameObject.Find("MiniGameCanvas").GetComponent<Canvas>();
        MiniGameCanvas.enabled = true;
        
        if (miniGameManager != null)
        {
            miniGameManager.StartGame(Settings);
            

            while (!DTT.MinigameMemory.MemoryGameManager.IsGameCompleted)
            {
                await Cysharp.Threading.Tasks.UniTask.DelayFrame(1, cancellationToken: asyncToken.CancellationToken);
            }
        }
        else
        {
            Debug.LogWarning("Міні-гра не знайдена в сцені.");
        }

        MiniGameCanvas.enabled = false;
    }
}
