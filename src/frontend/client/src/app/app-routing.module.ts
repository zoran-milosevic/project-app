import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
    { path: '', redirectTo: 'database', pathMatch: 'full' },
    { path: 'database', loadChildren: () => import('@feature/home/home.module').then(m => m.HomeModule) },
    { path: 'file', loadChildren: () => import('@feature/file/file.module').then(m => m.FileModule) },
    { path: 'user-input', loadChildren: () => import('@feature/user-input/user-input.module').then(m => m.UserInputModule) }
];

@NgModule({
    imports: [
        RouterModule.forRoot(routes)
    ],
    exports: [
        RouterModule
    ]
})
export class AppRoutingModule { }
