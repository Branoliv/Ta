import { NgModule } from '@angular/core';
import { IonicPageModule } from 'ionic-angular';
import { EditarUsuarioPage } from './editar-usuario';

@NgModule({
  declarations: [
    EditarUsuarioPage,
  ],
  imports: [
    IonicPageModule.forChild(EditarUsuarioPage),
  ],
})
export class EditarUsuarioPageModule {}
