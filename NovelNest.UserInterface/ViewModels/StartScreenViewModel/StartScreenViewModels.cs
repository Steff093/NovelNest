using Microsoft.EntityFrameworkCore;
using NovelNest.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovelNest.UserInterface.ViewModels.StartScreenViewModel
{
    public class StartScreenViewModels : BaseViewModel
    {
        private double _progress;

        public StartScreenViewModels()
        {
            LoadProgressBar();
        }

        public double Progress
        {
            get => _progress;
            set
            {
                _progress = value;
                OnPropertyChanged(nameof(Progress));
            }
        }

        public async Task LoadDatabaseAsync()
        {
            using (var db = new NovelNestDataContext())
            {
                await db.BookEntities.LoadAsync();
                await db.FolderEntities.LoadAsync();
            }
        }

        public async Task LoadProgressBar()
        {
            int loadProgressbar = 10;
            for (int i = 0; i < loadProgressbar; i++)
            {
                await Task.Delay(500);
                Progress = (double)(i +1) / loadProgressbar * 100;
            }
            await LoadDatabaseAsync();
        }
    }

}
