import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';

import { User } from '../../services/api/user.service';
import { Movie, MovieService } from '../../services/api/movie.service';
import { NavbarComponent } from '../navbar/navbar.component';

export interface MovieWithOptions extends Movie {
  options: boolean;
}

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule, NavbarComponent, RouterModule],
  templateUrl: './profile.component.html',
})
export class ProfileComponent implements OnInit {
  user: User = localStorage.getItem('user') ? JSON.parse(localStorage.getItem('user') as string) : null;
  movies: MovieWithOptions[] = [];

  constructor(private movieService: MovieService, private router: Router) {}

  ngOnInit(): void {
    this.movieService.getMoviesForUser().subscribe((movies) => {
      this.movies = movies.map(movie => ({ ...movie, options: false }));
    });
  }

  navigateToMoviePage(id: number): void {
    this.router.navigate([`/movie/${id}`]);
  }

  navigateToEditMovie(id: number): void {
    this.router.navigate([`/edit-movie/${id}`]);
  }

  deleteMovie(id: number): void {
    this.movieService.deleteMovie(id).subscribe(() => {
      this.movies = this.movies.filter((movie) => movie.id !== id);
    });
  }

  seeOptions(movie: MovieWithOptions) {
    movie.options = !movie.options;
    if (movie.options) {
      document.addEventListener('click', this.closeDropdownOnClickOutside.bind(this, movie));
    }
  }

  closeDropdown(movie: MovieWithOptions) {
    movie.options = false;
    document.removeEventListener('click', this.closeDropdownOnClickOutside.bind(this, movie));
  }

  closeDropdownOnClickOutside(movie: MovieWithOptions, event: Event) {
    const target = event.target as HTMLElement;
    if (!target.closest('.dropdown-container')) {
      this.closeDropdown(movie);
    }
  }
}
