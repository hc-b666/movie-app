import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class MovieService {
  private apiUrl = `${environment.apiUrl}/movies`;

  constructor(private http: HttpClient) {}

  getMovies(): Observable<Movie[]> {
    return this.http.get<Movie[]>(this.apiUrl);
  }

  addMovie(movie: Movie): Observable<any> {
    return this.http.post(this.apiUrl, movie);
  }

  getMovieById(id: number): Observable<Movie> {
    return this.http.get<Movie>(`${this.apiUrl}/${id}`);
  }

  updateMovie(id: number, movie: Movie): Observable<any> {
    return this.http.put(`${this.apiUrl}/${id}`, movie);
  }

  deleteMovie(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }

  getMoviesForUser(): Observable<Movie[]> {
    return this.http.get<Movie[]>(`${this.apiUrl}/user`);
  }
}

export interface Movie {
  id: number;
  title: string;
  description: string;
  genre?: string;
  imageUrl?: string;
  releaseDate?: string;
  rating: string;
  country?: string;
  cast?: string;
  userId: number;
}
