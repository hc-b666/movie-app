import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { MovieService } from '../../services/api/movie.service';
import { NavbarComponent } from '../navbar/navbar.component';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-edit-movie',
  standalone: true,
  imports: [NavbarComponent, ReactiveFormsModule],
  templateUrl: './edit-movie.component.html',
})
export class EditMovieComponent implements OnInit {
  movie: any = {};
  movieForm: FormGroup;

  constructor(private movieService: MovieService, private route: ActivatedRoute, private fb: FormBuilder, private toasts: ToastrService) {
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

  ngOnInit(): void {
    const movieId = this.route.snapshot.paramMap.get('id');
    if (movieId) {
      this.movieService.getMovieById(Number(movieId)).subscribe((movie) => {
        this.movie = movie;
        this.movieForm.patchValue(this.movie);
      });
    }
  }

  goBack(): void {
    window.history.back();
  }

  onSubmit(): void {
    if (this.movieForm.valid) {
      this.movieService.updateMovie(this.movie.id, this.movieForm.value).subscribe(() => {
        this.movieForm.reset();
      });
    }
    this.goBack();
    this.toasts.success('Movie updated successfully');
  }
}
