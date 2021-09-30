import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { WebloginComponent } from './weblogin.component';
import { ReactiveFormsModule } from '@angular/forms';

const webLoginRoutes: Routes = [
  {path: '', component: WebloginComponent}
]

@NgModule({
  declarations: [WebloginComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(webLoginRoutes),
    ReactiveFormsModule
  ]
})
export class WebloginModule { }
