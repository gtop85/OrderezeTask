import React, { useState, useEffect } from 'react';

const initialFieldValues = {
    name: '',
    description: '',
    imageFile: '',
    imageSrc: ''
}

export default function Image(props) {
    const { addImage, recordForAdd } = props

    const [values, setValues] = useState(initialFieldValues)
    const [errors, setErrors] = useState({})

    useEffect(() => {
        if (recordForAdd != null)
            setValues(recordForAdd);
    }, [recordForAdd])

    const handleInputChange = e => {
        const { name, value } = e.target;
        setValues({
            ...values,
            [name]: value
        })
    }

    const showPreview = e => {
        if (e.target.files && e.target.files[0]) {
            let imageFile = e.target.files[0];
            const reader = new FileReader();
            reader.onload = x => {
                setValues({
                    ...values,
                    imageFile,
                    imageSrc: x.target.result
                })
            }
            reader.readAsDataURL(imageFile)
        }
        else {
            setValues({
                ...values,
                imageFile: null,
                imageSrc: ''
            })
        }
    }

    const validate = () => {
        let temp = {}
        temp.name = values.name === "" ? false : true;
        temp.imageSrc = values.imageSrc === "" ? false : true;

        var size = parseFloat(values.imageFile.size / (1024 * 1024)).toFixed(2);
        if (size > 2 || !values.imageFile.name?.match(/\.(jpg|png|gif)$/)) {
            temp.imageSrc = false;
            alert('Please select a .jpg, .png and .gif file up to 2MB');
        }
        if (values.name.length > 100){
            temp.name = false;
            alert('Please type a name up to 100 characters');
        } 
        if (values.description > 300){
            temp.description = false;
            alert('Please type a description up to 300 characters');
        } 
        setErrors(temp)
        return Object.values(temp).every(x => x === true)
    }

    const resetForm = () => {
        setValues(initialFieldValues)
        document.getElementById('image-uploader').value = null;
        setErrors({})
    }

    const handleFormSubmit = e => {
        e.preventDefault()
        if (validate()) {
            const formData = new FormData()
            formData.append('name', values.name);
            formData.append('description', values.description);
            formData.append('imageFile', values.imageFile);
            addImage(formData, resetForm);
        }
    }

    const applyErrorClass = field => ((field in errors && errors[field] === false) ? ' invalid-field' : '')


    return (
        <>
            <div className="container text-center">
                <p className="lead">Upload an image</p>
            </div>
            <form autoComplete="off" noValidate onSubmit={handleFormSubmit}>
                <div className="card">
                    <img alt="&#xf03e;" src={values.imageSrc} className="card-img-top preview img-thumbnail" />
                    <div className="card-body">
                        <div className="form-group">
                            <label>Select a .jpg, .png and .gif file up to 2MB</label>
                            <input type="file" accept="image/*" className={"form-control-file" + applyErrorClass('imageSrc')}
                                onChange={showPreview} id="image-uploader" />
                        </div>
                        <div className="form-group">
                            <input className={"form-control" + applyErrorClass('name')} placeholder="Image name" name="name"
                                value={values.name}
                                onChange={handleInputChange} />
                        </div>
                        <div className="form-group">
                            <input className="form-control" placeholder="Decription of your image" name="description"
                                value={values.description}
                                onChange={handleInputChange} />
                        </div>
                        <div className="form-group text-center">
                            <button type="submit" className="btn btn-primary">Submit</button>
                        </div>
                    </div>
                </div>
            </form>
        </>
    )
}

