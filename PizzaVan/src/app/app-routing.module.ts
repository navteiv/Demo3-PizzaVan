import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ClientComponent } from './client/client.component';
import { AuthGuard } from './core/guard/auth.guard';
import { LoginComponent } from './login/login.component';

export const routes: Routes = [
  { path: '', redirectTo: 'client', pathMatch: 'full' },
  { path: 'client', loadChildren: () => import('./client/client.module').then(m => m.ClientModule)},
  { path: 'login', component: LoginComponent },
  { path: 'main', loadChildren: () => import('./main/main.module').then(m => m.MainModule), canActivate: [AuthGuard]},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
