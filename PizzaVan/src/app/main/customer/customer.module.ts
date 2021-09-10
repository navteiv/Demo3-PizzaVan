import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CustomerComponent } from './customer.component';
import { RouterModule, Routes } from '@angular/router';

const customerRoutes: Routes = [
  {path:'', redirectTo:'index', pathMatch:'full'},
  {path:'index', component: CustomerComponent}
]

@NgModule({
  declarations: [
    CustomerComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(customerRoutes)
  ]
})
export class CustomerModule { }
