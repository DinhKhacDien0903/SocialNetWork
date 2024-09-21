import { BrowserRouter, Route, Routes } from 'react-router-dom';
import routes from './routes';

function App() {
    return (
        <BrowserRouter>
            <Routes>
                {routes.map((route, index) => {
                    const Page = route.component;
                    return <Route key={`route-${index}`} path={route.path} element={<Page />} />;
                })}
            </Routes>
        </BrowserRouter>
    );
}

export default App;
