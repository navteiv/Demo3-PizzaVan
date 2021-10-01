import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ClientComponent } from './client.component';
import { NgxPaginationModule } from 'ngx-pagination';
import { HistoryComponent } from './history/history.component';

const clientRoutes: Routes = [
  {
    path: '', component: ClientComponent, children:[
      {path: '',redirectTo: 'index', pathMatch: 'full'},
      {path: 'login', loadChildren: () => import('./weblogin/weblogin.module').then(m => m.WebloginModule)},
      {path: 'index', loadChildren: () => import('./index/index.module').then(m => m.IndexModule)},
      {path: 'history', loadChildren: () => import('./history/history.module').then(m => m.HistoryModule)}
    ]
  }
]

@NgModule({
  declarations: [
    HistoryComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(clientRoutes)
  ]
})
export class ClientModule { }
