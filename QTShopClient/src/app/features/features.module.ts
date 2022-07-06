import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DemoComponent } from './demo/demo.component';



@NgModule({
  declarations: [
    DemoComponent
  ],
  exports: [
    DemoComponent
  ],
  imports: [
    CommonModule
  ]
})
export class FeaturesModule { }
