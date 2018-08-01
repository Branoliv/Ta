import { BrowserModule } from '@angular/platform-browser';
import { ErrorHandler, NgModule } from '@angular/core';
import { IonicApp, IonicErrorHandler, IonicModule } from 'ionic-angular';

import { MyApp } from './app.component';

import { StatusBar } from '@ionic-native/status-bar';
import { SplashScreen } from '@ionic-native/splash-screen';
import { UtilService } from '../providers/util.services';
import { UsuarioService } from '../providers/usuario.service';
import { TarefaService } from '../providers/tarefa.service';
import { ListaDeTarefasService } from '../providers/listadetarefas.service';
import { HttpModule } from '../../node_modules/@angular/http';

@NgModule({
  declarations: [
    MyApp

  ],
  imports: [
    BrowserModule,
    HttpModule,
    IonicModule.forRoot(MyApp)
  ],
  bootstrap: [IonicApp],
  entryComponents: [
    MyApp
  ],
  providers: [
    StatusBar,
    SplashScreen,
    UtilService,
    UsuarioService,
    TarefaService,
    ListaDeTarefasService,
    { provide: ErrorHandler, useClass: IonicErrorHandler }
  ]
})
export class AppModule { }
