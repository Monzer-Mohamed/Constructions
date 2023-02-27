import axios from 'axios';
 

export const apiPost=(uri,body,params=undefined)=>{
    return axios.post(process.env.REACT_APP_BaseUrl+uri,body,{
        headers: {'x-Gateway-APIKey':process.env.REACT_APP_APIKey},
        params
    })
    .catch(function (error) {
      console.log(error);
    });
}
