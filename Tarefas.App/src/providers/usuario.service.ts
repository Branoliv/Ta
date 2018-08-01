import { UtilService } from './util.services';
import { Injectable } from "@angular/core";
import { Headers, Http, Response } from '@angular/http';
import 'rxjs/add/operator/toPromise';

@Injectable()
export class UsuarioService {

    constructor(
        public http: Http,
        public utilService: UtilService,
    ) { }

    autenticar(request: any): Promise<Response> {
        let host = this.utilService.obterHostDaApi();

        let headers: any = new Headers();
        headers.append('Content-Type', 'application/json');
        return this.http.post(host + 'api/v1/Usuario/Autenticar', request, headers).toPromise();
    }

    adicionar(form: any): Promise<Response> {

        let host = this.utilService.obterHostDaApi();
        let headers: any = new Headers();
        headers.append('Content-Type', 'application/json');

        return this.http.post(host + 'api/v1/Usuario/Adicionar', form, { headers: headers }).toPromise();
    }

    obterUsuario(idUsuario: string): Promise<Response> {

        let host = this.utilService.obterHostDaApi();

        let headers = new Headers();
        headers.append('Content-Type', 'application/json');
        headers.append('Authorization', 'Bearer ' + localStorage.getItem('tarefaToken'));

        return this.http.get(host + 'api/v1/Usuario/Obter/' + idUsuario, { headers: headers }).toPromise();

    }

    atualizarUsuario(request: any): Promise<Response> {
        let host = this.utilService.obterHostDaApi();
        let headers: any = new Headers();
        headers.append('Content-Type', 'application/json');
        headers.append('Authorization', 'Bearer ' + localStorage.getItem('tarefaToken'));

        return this.http.post(host + 'api/v1/Usuario/Atualizar/', request, { headers: headers }).toPromise();
    }
}