import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterOutlet } from '@angular/router';
import { Weatherdashboard } from "./components/weatherdashboard/weatherdashboard";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet,
    Weatherdashboard],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected title = 'weatherDashBoard';
}
