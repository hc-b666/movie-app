import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MovieService, Movie } from '../../services/api/movie.service';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { NavbarComponent } from '../navbar/navbar.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, NavbarComponent],
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  movies: Movie[] = [];
  movieForm: FormGroup;

  constructor(private movieService: MovieService, private fb: FormBuilder) {
    this.movieForm = this.fb.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
      genre: [''],
      imageUrl: ['', Validators.required],
      releaseDate: [''],
      rating: ['', Validators.required],
      country: [''],
      cast: [''],
    });
  }

  ngOnInit(): void {
    this.getMovies();
  }
  
  getMovies(): void {
    this.movieService.getMovies().subscribe((movies) => {
      this.movies = movies;
    });
  }

  onSubmit(): void {
    if (this.movieForm.valid) {
      this.movieService.addMovie(this.movieForm.value).subscribe(() => {
        this.getMovies();
        this.movieForm.reset();
      });
    }
  }
}
