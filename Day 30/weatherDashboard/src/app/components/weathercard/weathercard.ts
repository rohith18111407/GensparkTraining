import { Component, Input, NgModule } from '@angular/core';
import { WeatherData } from '../../models/weather.model';
import { FormsModule } from '@angular/forms';
import { CommonModule, NgIf } from '@angular/common';
import { MatCardModule } from '@angular/material/card';

@Component({
  selector: 'app-weathercard',
  imports: [CommonModule, MatCardModule],
  templateUrl: './weathercard.html',
  styleUrl: './weathercard.css'
})
export class Weathercard {
  @Input() data!: WeatherData;
}
