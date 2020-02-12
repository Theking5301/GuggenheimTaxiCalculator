import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { TaxiCostCalculatorComponent } from './components/taxi-cost-calculator/taxi-cost-calculator.component';

const routes: Routes = [{ path: '', component: TaxiCostCalculatorComponent }];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
