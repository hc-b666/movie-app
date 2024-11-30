import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';

import { AuthService } from '../../services/auth/auth.service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './navbar.component.html',
})
export class NavbarComponent {
  authenticated = false;

  constructor(private authService: AuthService) {
    this.authenticated = !!localStorage.getItem('token');
  }

  logout(): void {
    this.authService.logout();
  }

}
