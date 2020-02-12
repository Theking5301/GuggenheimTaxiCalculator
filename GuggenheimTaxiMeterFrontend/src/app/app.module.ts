import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { DatePipe } from '@angular/common';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http'; 

import { TaxiCostCalculatorComponent } from './components/taxi-cost-calculator/taxi-cost-calculator.component';
import { TaxiCostCalculatorService } from './services/taxi-cost-calculator/taxi-cost-calculator.service';
import { DateValidatorDirective } from './directives/date-validator/date-validator.directive';
import { TimeValidatorDirective } from './directives/time-validator/time-validator.directive';
import { NumberValidatorDirective } from './directives/number-validator/number-validator.directive';

@NgModule({
  declarations: [
    AppComponent,
    TaxiCostCalculatorComponent,
    DateValidatorDirective,
    TimeValidatorDirective,
    NumberValidatorDirective
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [DatePipe, TaxiCostCalculatorService],
  bootstrap: [AppComponent]
})
export class AppModule { }
