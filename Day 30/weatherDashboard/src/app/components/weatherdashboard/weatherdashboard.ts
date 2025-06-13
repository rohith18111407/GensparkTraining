import { Component } from '@angular/core';
import { WeatherService } from '../../services/weather.service';
import { WeatherData } from '../../models/weather.model';
import { Observable } from 'rxjs';
import { BrowserModule } from '@angular/platform-browser';
import { Weathercard } from '../weathercard/weathercard';
import { Citysearch } from '../citysearch/citysearch';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-weatherdashboard',
  imports: [CommonModule, Weathercard, Citysearch],
  templateUrl: './weatherdashboard.html',
  styleUrl: './weatherdashboard.css'
})
export class Weatherdashboard {
  weather$: Observable<WeatherData | null>;
  error = false;

  constructor(private weatherService: WeatherService) {
    this.weather$ = this.weatherService.weather$;
    this.weather$.subscribe(data => this.error = data === null);
  }
}
