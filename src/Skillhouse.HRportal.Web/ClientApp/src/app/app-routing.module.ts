import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './core/guards/auth.guard';
import { RoleGuard } from './core/guards/role.guard';


const routes: Routes = [
  { path: 'login', loadChildren: () => import('./auth/login.module').then(m => m.LoginModule) },
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full',
  },
  {
    path: 'home', loadChildren: () => import('./home/home.module').then(m => m.HomeModule),
    //canActivate: [AuthGuard, RoleGuard]
  },
  
  
  
  
  
 
    
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
