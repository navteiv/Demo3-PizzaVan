import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DishComponent } from './dish.component';
import { RouterModule, Routes } from '@angular/router';

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
    RouterModule.forChild(dishRoutes)
  ]
})
export class DishModule { }
