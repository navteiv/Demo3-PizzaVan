import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { MainComponent } from './main.component';

const mainRoutes: Routes = [
  {
    path: '', component: MainComponent, children: [
      {path:'', redirectTo: 'user', pathMatch: 'full'},
      {path:'customer', loadChildren: () => import('./customer/customer.module').then(m => m.CustomerModule)},
      {path: 'user', loadChildren: () => import('./user/user.module').then(m => m.UserModule)},
      {path: 'dish', loadChildren: () => import('./dish/dish.module').then(m => m.DishModule)},
      {path: 'order', loadChildren: () => import('./order/order.module').then(m => m.OrderModule)}
    ]
  }
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(mainRoutes)
  ]
})
export class MainModule { }
