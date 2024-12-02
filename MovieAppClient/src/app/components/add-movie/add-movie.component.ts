import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import {
  ReactiveFormsModule,
  FormBuilder,
  FormGroup,
  Validators,
} from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

import { NavbarComponent } from '../navbar/navbar.component';
import { MovieService } from '../../services/api/movie.service';

@Component({
  selector: 'app-add-movie',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    NavbarComponent,
  ],
  templateUrl: './add-movie.component.html',
})
export class AddMovieComponent {
  movieForm: FormGroup;

  constructor(
    private movieService: MovieService,
    private fb: FormBuilder,
    private toastr: ToastrService
  ) {
    this.movieForm = this.fb.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
      genre: ['', Validators.required],
      imageUrl: ['', Validators.required],
      releaseDate: ['', Validators.required],
      rating: ['', Validators.required],
      country: ['', Validators.required],
      cast: ['', Validators.required],
    });
  }

  onSubmit(): void {
    if (this.movieForm.valid) {
      this.movieService.addMovie(this.movieForm.value).subscribe(() => {
        this.movieForm.reset();
        this.toastr.success('Movie added successfully', 'Success!');
      });
    } else {
      this.toastr.error('Please fill in all required fields', 'Error');
    }
  }
}
