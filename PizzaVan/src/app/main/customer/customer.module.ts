import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CustomerComponent } from './customer.component';
import { RouterModule, Routes } from '@angular/router';
import { ExcelModule, GridModule, PDFModule } from '@progress/kendo-angular-grid';

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
    RouterModule.forChild(customerRoutes),
    GridModule,
    PDFModule,
    ExcelModule
  ]
})
export class CustomerModule { }
