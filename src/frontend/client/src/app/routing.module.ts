import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
    { path: '', redirectTo: 'database', pathMatch: 'full' },
    { path: 'database', loadChildren: () => import('./modules/main/main.module').then(m => m.MainModule) },
    { path: 'file', loadChildren: () => import('./modules/file/file.module').then(m => m.FileModule) },
    { path: 'user-input', loadChildren: () => import('./modules/user-input/user-input.module').then(m => m.UserInputModule) }
];

@NgModule({
    imports: [
        RouterModule.forRoot(routes)
    ],
    exports: [
        RouterModule
    ]
})
export class RoutingModule { }
