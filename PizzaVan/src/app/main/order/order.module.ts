import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrderComponent } from './order.component';
import { RouterModule, Routes } from '@angular/router';

const orderRoutes: Routes = [
  {path:'', redirectTo: 'index', pathMatch: 'full'},
  {path: 'index', component: OrderComponent}
]

@NgModule({
  declarations: [
    OrderComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(orderRoutes)
  ]
})
export class OrderModule { }
