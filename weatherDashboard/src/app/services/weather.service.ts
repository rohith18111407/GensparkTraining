import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { BehaviorSubject, ReplaySubject, catchError, throwError } from 'rxjs';
import { WeatherData } from '../models/weather.model';

@Injectable({ providedIn: 'root' })
export class WeatherService {
  private apiKey = '408ccd9a609e435445cec7bfdb1f9904';

  private weatherSubject = new BehaviorSubject<WeatherData | null>(null);
  weather$ = this.weatherSubject.asObservable();

  private historySubject = new ReplaySubject<string[]>(1);
  history$ = this.historySubject.asObservable();

  private maxHistory = 5;
  private storageKey = 'weatherHistory';
  private isBrowser: boolean;

  constructor(private http: HttpClient) {
    this.isBrowser = typeof window !== 'undefined' && !!window.localStorage;

    const initialHistory = this.isBrowser
      ? JSON.parse(localStorage.getItem(this.storageKey) || '[]')
      : [];
    this.historySubject.next(initialHistory);
  }

  fetchWeather(city: string): void {
    this.http
      .get<WeatherData>(
        `https://api.openweathermap.org/data/2.5/weather?q=${city}&units=metric&appid=${this.apiKey}`
      )
      .pipe(
        catchError((err) => {
          this.weatherSubject.next(null);
          return throwError(() => err);
        })
      )
      .subscribe((data) => {
        this.weatherSubject.next(data);
        this.addToHistory(city);
      });
  }

  private addToHistory(city: string) {
    if (!this.isBrowser) return;

    const history = JSON.parse(localStorage.getItem(this.storageKey) || '[]') as string[];

    const updated = [city, ...history.filter((c) => c.toLowerCase() !== city.toLowerCase())].slice(
      0,
      this.maxHistory
    );

    localStorage.setItem(this.storageKey, JSON.stringify(updated));
    this.historySubject.next(updated);
  }
}
