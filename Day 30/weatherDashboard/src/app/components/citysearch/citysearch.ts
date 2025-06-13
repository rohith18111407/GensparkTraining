import { Component, NgModule } from '@angular/core';
import { WeatherService } from '../../services/weather.service';
import { FormsModule, NgModel } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-citysearch',
  imports: [CommonModule, FormsModule],
  templateUrl: './citysearch.html',
  styleUrl: './citysearch.css'
})
export class Citysearch {
  city = '';
  history: string[] = [];

  constructor(private weatherService: WeatherService) {
    this.weatherService.history$.subscribe(h => this.history = h);
  }

  search() {
    if (this.city.trim()) {
      this.weatherService.fetchWeather(this.city.trim());
    }
  }

  searchFromHistory(city: string) {
    this.weatherService.fetchWeather(city);
  }
}
