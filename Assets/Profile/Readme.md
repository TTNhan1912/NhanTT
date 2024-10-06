# PROFILE

## Tổng Quát: 
UserProfile: Thông tin người chơi

ProfileData: Quản lý thông tin người chơi

ProfileUI: Xử lý hiển thị thông tin người chơi

## Mô tả:
### UserProfile:
Khai báo:
- string Name: Tên người chơi

- int Level: Cấp người chơi

- int ExperiencePoint:  Điểm kinh nghiệm hiện có

- int AvatarIndex: Ảnh đại diện của người chơi được lưu dưới dạng Index của 1 danh sách nào đó, logic triển khai theo danh sách này

### ProfileData:
Khai báo:
- UserProfile User: Biến lưu trữ thông tin của người chơi

- Action OnObtainedExperiencePoint: Action được gọi khi người chơi nhận được điểm kinh nghiệm

Chức năng:
- void Load()/Save(): Tải/lưu lại thông tin người chơi

- void SetName()/SetAvatar(): Chỉnh sửa thông tin người chơi 

- int GetRequirementExperiencePoint(): Trả về số điểm kinh nghiệm cần để lên cấp

- void ObtainExperiencePoint(int [point]): Nhận [point] điểm kinh nghiệm

- void LevelUp(): Lên cấp

- void LevelUpDirectly(): Vẫn là lên cấp nhưng có thể gọi một cách trực tiếp