import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CapitalizeFirstLetterPipe } from 'src/CustomeValidations&Pipe/capitalize-first-letter.pipe';

@NgModule({
  declarations: [CapitalizeFirstLetterPipe],
  exports: [CapitalizeFirstLetterPipe],
})
export class SharedModule {}
