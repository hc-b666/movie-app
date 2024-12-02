import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';

import { AuthService } from '../../services/auth/auth.service';
import { User } from '../../services/api/user.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './navbar.component.html',
})
export class NavbarComponent {
  authenticated = false;
  user: User = localStorage.getItem('user')
    ? JSON.parse(localStorage.getItem('user') as string)
    : null;

  constructor(private authService: AuthService, private toastr: ToastrService) {
    this.authenticated = !!localStorage.getItem('token');
  }

  logout(): void {
    this.authService.logout();
    this.toastr.success('Successfully logged out');
  }
}
