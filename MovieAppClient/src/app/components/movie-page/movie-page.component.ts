import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { switchMap } from 'rxjs/operators';

import { MovieService } from '../../services/api/movie.service';
import { UserService } from '../../services/api/user.service';
import { NavbarComponent } from '../navbar/navbar.component';

@Component({
  selector: 'app-movie-page',
  standalone: true,
  imports: [NavbarComponent, CommonModule],
  templateUrl: './movie-page.component.html',
})
export class MoviePageComponent implements OnInit {
  movie: any = {};
  user: any = {};

  constructor(
    private movieService: MovieService,
    private userService: UserService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    const movieId = this.route.snapshot.paramMap.get('id');
    if (movieId) {
      this.movieService.getMovieById(Number(movieId)).pipe(
        switchMap((movie) => {
          this.movie = movie;
          return this.userService.getUserById(movie.userId);
        })
      ).subscribe((user) => {
        this.user = user;
      });
    }
  }

  goBack(): void {
    window.history.back();
  }
}
