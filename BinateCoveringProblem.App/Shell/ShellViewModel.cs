using BinateCoveringProblem.App.Eventing;
using BinateCoveringProblem.App.Eventing.Events;
using BinateCoveringProblem.App.Matrix;
using BinateCoveringProblem.App.Matrix.Extensions;
using BinateCoveringProblem.App.Matrix.Settings;
using BinateCoveringProblem.Core.Algorithms.Covering;
using BinateCoveringProblem.Core.Extensions;
using Caliburn.Micro;

namespace BinateCoveringProblem.App.Shell
{
    public class ShellViewModel : Screen
    {
        public IMatrixViewModel Matrix { get; }
        public IMatrixSettingsViewModel MatrixSettings { get; }

        private string result;
        public string Result
        {
            get
            {
                return result;
            }
            set
            {
                if (value == result)
                    return;

                result = value;
                NotifyOfPropertyChange(() => Result);
            }
        }

        public ShellViewModel(
            IMatrixViewModel matrix,
            IMatrixSettingsViewModel matrixSettings,
            IEventStream eventStream)
        {
            this.Matrix = matrix;
            this.MatrixSettings = matrixSettings;

            eventStream.Subscribe<MatrixChanged>(OnMatrixChanged);
        }
        
        public void Solve()
        {
            var source = Matrix.ToTable().ToDictionary();

            ICoveringAlgorithm covering = source.IsBinate() 
                ? new BinateCovering(source) 
                : new UnateCovering(source);
            
            Result = covering.Result.Print();
        }

        protected override void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);

            MatrixSettings.Initialize();
        }

        private void OnMatrixChanged(MatrixChanged e)
        {
            Result = string.Empty;
        }
    }
}
