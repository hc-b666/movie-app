import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from './environments/environment';

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
}

export interface Movie {
  id?: number;
  title: string;
  description: string;
  genre?: string;
  imageUrl?: string;
  releaseDate?: string;
  rating: string;
  country?: string;
  cast?: string;
}
