import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ClienteComponent } from './cliente/cliente.component';
import { DividaComponent } from './divida/divida.component';
import { ParametroComponent } from './parametro/parametro.component';
import { ComissaoComponent } from './comissao/comissao.component';

const routes: Routes = [
  { path: '', redirectTo: '/', pathMatch: 'full' },
  { path: 'cliente', component: ClienteComponent },
  { path: 'divida', component: DividaComponent },
  { path: 'parametro', component: ParametroComponent },
  { path: 'comissao', component: ComissaoComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
