import React, { useEffect, useState } from 'react';
import axios from 'axios';
import 'bootstrap/dist/css/bootstrap.min.css';

const Portfolio = () => {
  const [profileData, setProfileData] = useState(null);
  const [editingProfile, setEditingProfile] = useState(false);
  const [editedProfile, setEditedProfile] = useState(null);
  const [editingHobby, setEditingHobby] = useState(null);
  const [editingFramework, setEditingFramework] = useState(null);


  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await axios.get('http://localhost:5065/api/profile');
        setProfileData(response.data);
      } catch (error) {
        console.error('Error fetching data:', error);
      }
    };

    fetchData();
  }, []);

  const handleEditClick = () => {
    setEditedProfile({ ...profileData });
    setEditingProfile(true);
  };

  const handleSaveClick = async () => {
    try {
      const editedData = {
        profileId: editedProfile.profileId,
        name: editedProfile.name,
        lastname: editedProfile.lastname,
        email: editedProfile.email,
        city: editedProfile.city,
        country: editedProfile.country,
        summary: editedProfile.summary,
        instagram: editedProfile.instagram,
        facebook: editedProfile.facebook,
        age: editedProfile.age,
        imageUrl: editedProfile.imageUrl,
      };

      await axios.put(`http://localhost:5065/api/profile/${editedProfile.profileId}`, editedData);
      editedData.frameworks = editedProfile.frameworks;
      editedData.hobbies = editedProfile.hobbies;
      setProfileData(editedData);
      setEditingProfile(false);
    } catch (error) {
      console.error('Error updating profile:', error);
    }
  };

  const handleDeleteHobby = async (hobbyId) => {
    try {
      await axios.delete(`http://localhost:5065/api/profile/hobby/${hobbyId}`);
      setProfileData({
        ...profileData,
        hobbies: profileData.hobbies.filter((hobby) => hobby.hobbyId !== hobbyId),
      });
    } catch (error) {
      console.error('Error deleting hobby:', error);
    }
  };

  const handleDeleteFramework = async (frameworkId) => {
    try {
      await axios.delete(`http://localhost:5065/api/profile/framework/${frameworkId}`);
      setProfileData({
        ...profileData,
        frameworks: profileData.frameworks.filter((framework) => framework.frameworkId !== frameworkId),
      });
    } catch (error) {
      console.error('Error deleting framework:', error);
    }
  };

  const handleEditHobby = (hobby) => {
    setEditingHobby({ ...hobby });
  };

  const handleEditFramework = (framework) => {
    setEditingFramework({ ...framework });
  };

  const handleSaveEditedHobby = async () => {
    try {
      const editedData = {
        hobbyId: editingHobby.hobbyId,
        name: editingHobby.name,
        description: editingHobby.description,
        profileId: profileData.profileId,
        profile: {
          profileId: profileData.profileId,
          name: profileData.name,
          lastname: profileData.lastname,
          email: profileData.email,
          city: profileData.city,
          country: profileData.country,
          summary: profileData.summary,
          instagram: profileData.instagram,
          facebook: profileData.facebook,
          age: profileData.age,
          imageUrl: profileData.imageUrl,
        },
      };

      await axios.put(`http://localhost:5065/api/profile/hobby/${editingHobby.hobbyId}`, editedData);
      setProfileData({
        ...profileData,
        hobbies: profileData.hobbies.map((hobby) =>
          hobby.hobbyId === editingHobby.hobbyId ? editingHobby : hobby
        ),
      });
      setEditingHobby(null);
    } catch (error) {
      console.error('Error updating hobby:', error);
    }
  };

  const handleSaveEditedFramework = async () => {
    try {
      const editedData = {
        frameworkId: editingFramework.frameworkId,
        name: editingFramework.name,
        level: editingFramework.level,
        year: editingFramework.year,
        quantity: editingFramework.quantity,
        profileId: profileData.profileId,
        profile: {
          profileId: profileData.profileId,
          name: profileData.name,
          lastname: profileData.lastname,
          email: profileData.email,
          city: profileData.city,
          country: profileData.country,
          summary: profileData.summary,
          instagram: profileData.instagram,
          facebook: profileData.facebook,
          age: profileData.age,
          imageUrl: profileData.imageUrl,
        },
      };

      await axios.put(`http://localhost:5065/api/profile/framework/${editingFramework.frameworkId}`, editedData);
      setProfileData({
        ...profileData,
        frameworks: profileData.frameworks.map((framework) =>
          framework.frameworkId === editingFramework.frameworkId ? editingFramework : framework
        ),
      });
      setEditingFramework(null);
    } catch (error) {
      console.error('Error updating framework:', error);
    }
  };

  const handleModalInputChange = (e) => {
    setEditedProfile({ ...editedProfile, [e.target.name]: e.target.value });
  };

  const handleHobbyInputChange = (e) => {
    setEditingHobby({ ...editingHobby, [e.target.name]: e.target.value });
  };

  const handleFrameworkInputChange = (e) => {
    setEditingFramework({ ...editingFramework, [e.target.name]: e.target.value });
  };

  return (
    <div className="container mt-5">
      {profileData && (
        <div>
          <div className="row mt-4">
            <div className="col-md-4">
              <div className="card border border-3 border-primary">
                <img src={profileData.imageUrl} className="img-fluid rounded-start border border-1 border-primary" alt="mi foto" />
              </div>
            </div>
            <div className="col-md-8">
              <div className="card mb-5">
                <div className="card-body">
                  <h5 className="card-title">
                    {editingProfile ? (
                      <>
                      <input
                        type="text"
                        name="name"
                        value={editedProfile.name}
                        onChange={handleModalInputChange}
                      />
                      <input
                        type="text"
                        name="lastname"
                        value={editedProfile.lastname}
                        onChange={handleModalInputChange}
                      />
                    </>
                      
                    ) : (
                      `${profileData.name || ''} ${profileData.lastname || ''}`
                    )}
                  </h5>
                  <p className="card-text">
                    {editingProfile ? (
                      <textarea
                        name="summary"
                        value={editedProfile.summary}
                        onChange={handleModalInputChange}
                      />
                    ) : (
                      profileData.summary || ''
                    )}
                  </p>
                </div>
              </div>
              {editingProfile ? (
                <button className="btn btn-primary" onClick={handleSaveClick}>
                  Guardar
                </button>
              ) : (
                <button className="btn btn-primary" onClick={handleEditClick} data-bs-toggle="modal" data-bs-target="#editModal">
                  Editar
                </button>
              )}
            </div>
          </div>

          <section className="info">
            <ul className="list-group">
              <li className="list-group-item active" aria-current="true">
                Datos Personales
              </li>
              <li className="list-group-item">
                Edad: {editingProfile ? (
                  <input
                    type="number"
                    name="age"
                    value={editedProfile.age}
                    onChange={handleModalInputChange}
                  />
                ) : (
                  profileData.age || ''
                )} años
              </li>
              <li className="list-group-item">
                Ciudad de origen: {editingProfile ? (
                  <input
                    type="text"
                    name="city"
                    value={editedProfile.city}
                    onChange={handleModalInputChange}
                  />
                ) : (
                  profileData.city || ''
                )}
              </li>
              <li className="list-group-item">
                Correo electrónico: {editingProfile ? (
                  <input
                    type="email"
                    name="email"
                    value={editedProfile.email}
                    onChange={handleModalInputChange}
                  />
                ) : (
                  <a href={`mailto:${profileData.email || ''}`}>{profileData.email || ''}</a>
                )}
              </li>
              <li className="list-group-item">
                Instagram: {editingProfile ? (
                  <input
                    type="text"
                    name="instagram"
                    value={editedProfile.instagram}
                    onChange={handleModalInputChange}
                  />
                ) : (
                  <a href={profileData.instagram || ''}>{profileData.instagram || ''}</a>
                )}
              </li>
              <li className="list-group-item">
                Facebook: {editingProfile ? (
                  <input
                    type="text"
                    name="facebook"
                    value={editedProfile.facebook}
                    onChange={handleModalInputChange}
                  />
                ) : (
                  <a href={profileData.facebook || ''}>{profileData.facebook || ''}</a>
                )}
              </li>
            </ul>
          </section>

          <section className="intereses mt-4">
            <ul className="list-group">
              <li className="list-group-item list-group-item-success">Intereses</li>
              {profileData.hobbies?.map((hobby, index) => (
                <li className="list-group-item list-group-item-light d-flex justify-content-between" key={index}>
                  {editingHobby?.hobbyId === hobby.hobbyId ? (
                    <>
                      <input
                        type="text"
                        name="description"
                        value={editingHobby.description}
                        onChange={handleHobbyInputChange}
                      />
                      <button
                        className="btn btn-primary btn-sm"
                        onClick={handleSaveEditedHobby}
                      >
                        Guardar
                      </button>
                    </>
                  ) : (
                    <>
                      {hobby.description}
                      <button
                        className="btn btn-primary btn-sm"
                        onClick={() => handleEditHobby(hobby)}
                      >
                        Editar
                      </button>
                      <button
                        className="btn btn-danger btn-sm"
                        onClick={() => handleDeleteHobby(hobby.hobbyId)}
                      >
                        Eliminar
                      </button>
                    </>
                  )}
                </li>
              ))}
            </ul>
          </section>

          <section className="tecnologias mt-4">
            <h3>Herramientas Tecnológicas</h3>
            <table className="table table-striped table-hover mt-4">
              <thead>
                <tr>
                  <th>Nombre</th>
                  <th>Nivel</th>
                  <th>Acciones</th>
                </tr>
              </thead>
              <tbody>
                {profileData.frameworks?.map((framework, index) => (
                  <tr key={index}>
                    <td>{framework.name}</td>
                    <td>
                      <div className="progress" role="progressbar" aria-label="Default striped example" aria-valuenow={framework.quantity} aria-valuemin="0" aria-valuemax="100">
                        <div className="progress-bar progress-bar-striped" style={{ width: `${framework.quantity}%` }}></div>
                      </div>
                    </td>
                    <td>
                      {editingFramework?.frameworkId === framework.frameworkId ? (
                        <>
                          <input
                            type="text"
                            name="name"
                            value={editingFramework.name}
                            onChange={handleFrameworkInputChange}
                          />
                          <button
                            className="btn btn-primary btn-sm"
                            onClick={handleSaveEditedFramework}
                          >
                            Guardar
                          </button>
                        </>
                      ) : (
                        <>
                          <button
                            className="btn btn-primary btn-sm"
                            onClick={() => handleEditFramework(framework)}
                          >
                            Editar
                          </button>
                          <button
                            className="btn btn-danger btn-sm"
                            onClick={() => handleDeleteFramework(framework.frameworkId)}
                          >
                            Eliminar
                          </button>
                        </>
                      )}
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </section>

          {/* Modal para editar */}
          <div className="modal fade" id="editModal" tabIndex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
            {/* ... (código del modal) */}
          </div>
        </div>
      )}
    </div>
  );
};

export default Portfolio;
