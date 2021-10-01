import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HistoryComponent } from './history.component';
import { RouterModule, Routes } from '@angular/router';

const historyRoutes: Routes = [
  {path: '', component: HistoryComponent}
]

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(historyRoutes)
  ]
})
export class HistoryModule { }
