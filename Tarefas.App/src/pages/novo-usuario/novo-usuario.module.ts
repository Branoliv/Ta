import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { NovoUsuarioPage } from './novo-usuario';

@NgModule({
  declarations: [
    NovoUsuarioPage,
  ],
  imports: [
    IonicPageModule.forChild(NovoUsuarioPage),
  ],
})
export class NovoUsuarioPageModule {}
