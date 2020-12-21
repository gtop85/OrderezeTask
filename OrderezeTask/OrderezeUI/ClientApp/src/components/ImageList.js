import React, { useState, useEffect } from 'react';
import axios from 'axios'
import Image from './Image';
import * as Constants from '../constants/constants';

export default function ImageList() {
    const [images, setImages] = useState([])

    useEffect(refreshImageList, [])

    const imageAPI = (url = Constants.API_URL) => {
        return {
            fetchAll: () => axios.get(url),
            create: newRecord => axios.post(url, newRecord),
            delete: id => axios.delete(url + id)
        }
    }

    function refreshImageList() {
        imageAPI().fetchAll()
            .then(res => setImages(res.data))
            .catch(err => console.log(err))
    }

    const addImage = (formData, onSuccess) => {
        
            imageAPI().create(formData)
                .then(res => {
                    onSuccess();
                    refreshImageList();
                })
                .catch(err => {
                    console.log(err)
                    alert('Something went wrong')
                })       
    }

    const onDelete = (e, id) => {
        if (window.confirm('Are you sure you want to delete this image?'))
            imageAPI().delete(id)
                .then(res => refreshImageList())
                .catch(err => console.log(err))
    }

    const imageCard = data => (
        <div className="card">
            <img alt="" src={data.imagePath} className="card-img-top" ></img>
            <div className="card-body">
                <h5>{data.name}</h5>
                <span>{data.description}</span><br/>
                <button className="btn btn-light delete-button" onClick={e => onDelete(e, parseInt(data.id))}>
                    <i className="far fa-trash-alt"></i>
                </button>
            </div>
        </div>
    )
    return (
        <div className="row">
            <div className="col-md-12">
                <div className="jumbotron jumbotron-fluid py-4">
                    <div className="container text-center">
                        <h1 className="display-4">Image Manager</h1>
                    </div>
                </div>
            </div>
        <div className="col-md-4">
                <Image
                    addImage={addImage}
                />
            </div>
        <div className="col-md-8">
            <div className="container text-center">
                <p className="lead">Uploaded images</p>
            </div>
            <table>
                <tbody>
                    {
                        [...Array(Math.ceil(images.length / 3))].map((e, i) =>
                            <tr key={i}>
                                <td>{imageCard(images[3 * i])}</td>
                                <td>{images[3 * i + 1] ? imageCard(images[3 * i + 1]) : null}</td>
                                <td>{images[3 * i + 2] ? imageCard(images[3 * i + 2]) : null}</td>
                            </tr>
                        )
                    }
                </tbody>
            </table>
        </div>
        </div>
    )
}