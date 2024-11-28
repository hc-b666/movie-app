import { Routes } from '@angular/router';
import { AuthGuard } from './auth.guard';
import { ProtectedComponent } from './protected/protected.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';

export const routes: Routes = [
  {
    path: 'protected-route',
    component: ProtectedComponent,
    canActivate: [AuthGuard],
  },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: '**', redirectTo: 'login' },
];
