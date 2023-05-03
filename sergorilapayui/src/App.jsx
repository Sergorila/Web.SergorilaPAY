import './App.css';
import {Routes, Route} from 'react-router-dom'
import Mainpage from './pages/mainpage/Mainpage';
import Authpage from './pages/authpage/Authpage';
import Userpage from './pages/userpage/Userpage';

function App() {
  return (
    <Routes>
      <Route index path='/' element={<Mainpage />}/>
      <Route path='/auth' element={<Authpage />}/>
      <Route path='/user/:id' element={<Userpage />}/>
      {/* 
      <Route path='/user/:id/orders' element={}/>
      <Route path='/checkout' element={}/> */}
    </Routes>
  );
}

export default App;
