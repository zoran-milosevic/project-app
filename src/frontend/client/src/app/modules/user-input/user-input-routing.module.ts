import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ContainerComponent } from '.';

const routes: Routes = [
    {
        path: '',
        component: ContainerComponent,
        children: [

        ]
    }
];

@NgModule({
    imports: [
        RouterModule.forChild(routes)
    ],
    exports: [
        RouterModule
    ]
})
export class UserInputRoutingModule { }
