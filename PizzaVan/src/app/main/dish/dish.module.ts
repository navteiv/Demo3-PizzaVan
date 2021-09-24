import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DishComponent } from './dish.component';
import { RouterModule, Routes } from '@angular/router';
import { ExcelModule, GridModule, PDFModule } from '@progress/kendo-angular-grid';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

const dishRoutes: Routes = [
  {path:'', redirectTo: 'index', pathMatch:'full'},
  {path:'index', component: DishComponent}
]

@NgModule({
  declarations: [
    DishComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(dishRoutes),
    GridModule,
    PDFModule,
    ExcelModule,
  ]
})
export class DishModule { }
