import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ClientComponent } from './client.component';
import { NgxPaginationModule } from 'ngx-pagination';

const clientRoutes: Routes = [
  {
    path: '', component: ClientComponent, children:[
      {path: '',redirectTo: 'index', pathMatch: 'full'},
      {path: 'login', loadChildren: () => import('./weblogin/weblogin.module').then(m => m.WebloginModule)},
      {path: 'index', loadChildren: () => import('./index/index.module').then(m => m.IndexModule)}
    ]
  }
]

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(clientRoutes)
  ]
})
export class ClientModule { }
