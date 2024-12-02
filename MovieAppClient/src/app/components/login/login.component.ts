import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

import { environment } from '../../environments/environment';
import { NavbarComponent } from '../navbar/navbar.component';
import { User } from '../../services/api/user.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule, NavbarComponent],
  templateUrl: './login.component.html',
  styleUrls: [],
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  private apiUrl = environment.apiUrl;

  constructor(
    private fb: FormBuilder,
    private http: HttpClient,
    private router: Router,
    private toastr: ToastrService
  ) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
    });
  }

  ngOnInit() {
    if (localStorage.getItem('token')) {
      this.router.navigate(['/']);
    }
  }

  onSubmit() {
    if (this.loginForm.valid) {
      this.http
        .post<{ token: string; user: User }>(
          `${this.apiUrl}/auth/login`,
          this.loginForm.value
        )
        .subscribe({
          next: (res) => {
            localStorage.setItem(
              'user',
              JSON.stringify({
                id: res.user.id,
                username: res.user.username,
                email: res.user.email,
              })
            );
            localStorage.setItem('token', res.token);
            this.router.navigate(['/']);
            this.toastr.success('Login successful', 'Success!');
          },
          error: (err) => {
            console.error('Login error', err);
            this.toastr.error('Invalid email or password', 'Error');
          },
        });
    }
  }
}
