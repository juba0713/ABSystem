using ABSystem.Data.Interfaces;
using ABSystem.Data.Models;
using ABSystem.Resources.Constants;
using ABSystem.Services.Dto;
using ABSystem.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ABSystem.Services.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;

        public RoomService(IRoomRepository roomRepository, IMapper mapper)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
        }

        public void AddRoom(RoomDto dto)
        {

            Room room = new Room();
            _mapper.Map(dto, room);
            room.CreatedDate = DateTime.Now;
            room.UpdatedDate = DateTime.Now;

            int id = this._roomRepository.AddRoom(room);

            SaveRoomImages(dto.Images, id);

        }

        public void EditRoom(RoomDto dto)
        {
            var existingRoom = this._roomRepository.GetRoomById(dto.Id);

            if (existingRoom == null)
            {
                throw new Exception(MessageConstant.ROOM_NOT_FOUND);
            }

            _mapper.Map(dto, existingRoom);
            existingRoom.UpdatedDate = DateTime.Now;

            this._roomRepository.EditRoom(existingRoom);

      
            SaveRoomImages(dto.Images, dto.Id);
            
        }

        public RoomDto GetRoomById(int roomId)
        {
            RoomDto dto = new RoomDto();

            var room = this._roomRepository.GetRoomById(roomId);

            if (room == null)
            {
                throw new Exception(MessageConstant.ROOM_NOT_FOUND);
            }

            _mapper.Map(room, dto);

            List<string> imagesPath = GetRoomImagesPath(roomId);

            dto.ImagesPath = imagesPath;

            dto.ImagePath = imagesPath.FirstOrDefault() ?? "";

            return dto;
        }

        public List<RoomDto> GetRoomsWithImage()
        {
            List<RoomDto> roomsDto = new List<RoomDto>();

            var rooms = this._roomRepository.GetRooms();

            if (rooms == null)
            {
                throw new Exception(MessageConstant.ROOM_NOT_FOUND);
            }

            foreach(var room in rooms)
            {
                RoomDto dto = new RoomDto();

                _mapper.Map(room, dto);

                dto.ImagePath = GetRoomImagesPath(room.Id).FirstOrDefault() ?? "";

                roomsDto.Add(dto);
            }

            return roomsDto;
        }

        public IEnumerable<Room> GetRooms()
        {
            return this._roomRepository.GetRooms();
        }

        public void DeleteRoom(int roomId)
        {
            var room = this._roomRepository.GetRoomById(roomId);

            Console.WriteLine(room);

            if(room == null)
            {
                throw new Exception(MessageConstant.ROOM_NOT_FOUND);
            }

            this._roomRepository.DeleteRoom(room);
        }

        /*
         * This is Helper method to save room images
         */
        public void SaveRoomImages(List<IFormFile>? images, int roomId)
        {
            if (images == null || images.Count == 0)
            {
                Console.WriteLine("No images to save.");
                return; // Exit the method if no images are provided
            }

            string specificRoomFolderPath = CommonConstant.RoomPath;

            string roomFolderPath = Path.Combine(specificRoomFolderPath, roomId.ToString());

            if (Directory.Exists(roomFolderPath))
            {
                var files = Directory.GetFiles(roomFolderPath);
                foreach (var file in files)
                {
                    File.Delete(file);
                }
            }
            else
            {
                // Create the folder if it doesn't exist
                Directory.CreateDirectory(roomFolderPath);
            }

            foreach (var image in images!)
            {
                Console.WriteLine("HELLO");
                if (image.Length > 0)
                {
                    Console.WriteLine("HI");
                    string filePath = Path.Combine(roomFolderPath, image.FileName);

                    // Save the file
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        image.CopyTo(stream);
                    }
                }
            }
        }

        /*
         * This is Helper method to get room images
         */
        public List<string> GetRoomImagesPath(int roomId)
        {
      
            string specificRoomFolderPath = CommonConstant.RoomPath;
            string roomFolderPath = Path.Combine(specificRoomFolderPath, roomId.ToString());


            string baseUrl = Path.Combine("/", CommonConstant.RoomFileName);

            List<string> imageUrls = new List<string>();

            if (Directory.Exists(roomFolderPath))
            {
                string[] files = Directory.GetFiles(roomFolderPath);

                foreach (var file in files)
                {

                    string fileName = Path.GetFileName(file);
                    string imageUrl = $"{baseUrl}/{roomId}/{fileName}";
                    Console.WriteLine(imageUrl);
                    imageUrls.Add(imageUrl);
                }
            }
            else
            {
                Console.WriteLine($"The folder for room {roomId} does not exist.");
            }

            return imageUrls;
        }

        public RoomDto GetRoomByIdWithBookings(int roomId)
        {
            RoomDto dto = new RoomDto();

            var room = this._roomRepository.GetRoomByIdWithBookings(roomId);

            if (room == null)
            {
                throw new Exception(MessageConstant.ROOM_NOT_FOUND);
            }

            _mapper.Map(room, dto);

            List<string> imagesPath = GetRoomImagesPath(roomId);

            dto.ImagesPath = imagesPath;

            dto.ImagePath = imagesPath.FirstOrDefault() ?? "";

            return dto;
        }

        public List<RoomDto> GetPopularRoomsWithImage()
        {
            List<RoomDto> roomsDto = new List<RoomDto>();

            var rooms = this._roomRepository.GetPopularRooms();

            if (rooms == null)
            {
                throw new Exception(MessageConstant.ROOM_NOT_FOUND);
            }

            foreach (var room in rooms)
            {
                RoomDto dto = new RoomDto();

                _mapper.Map(room, dto);

                dto.ImagePath = GetRoomImagesPath(room.Id).FirstOrDefault() ?? "";

                roomsDto.Add(dto);
            }

            return roomsDto;
        }
    }
}
