import { FooterComponent } from './components/footer/footer.component';
import { NavigationComponent } from './components/navigation/navigation.component';

import { MainModule } from './modules/main/main.module';
import { FileModule } from './modules/file/file.module';
import { UserInputModule } from './modules/user-input/user-input.module';

export const APP_MODULES: any[] = [
    MainModule,
    FileModule,
    UserInputModule
];

export const APP_COMPONENTS: any[] = [
    NavigationComponent,
    FooterComponent
];
